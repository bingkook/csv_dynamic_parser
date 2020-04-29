"use strict";
//VM  validation configuration
ko.validation.init({
    messagesOnModified: true,
    insertMessages: false,
    messageTemplate: null,
    errorClass: 'error'
});

// view model defination for the configuration data
function GetNewConfigurationItem() {
    return ko.validatedObservable({
        id: uuid(),
        name: ko.observable("").extend({ required: true }),
        dataType: ko.observable("").extend({ required: true }),
        required: ko.observable(false),
        size: ko.observable(0)
    });
}

// View model for the parser page
function ParserVM(para) {
    var self = this;
    self.step = $("#step");
    self.stepTitleArray = {
        step1: "Create configuration data items",
        step2: "Upload CSV file & Mapping",
        step3: "Parse Result"
    };
    self.dataTypeList = ko.observableArray([]);
    self.csvId = ko.observable("");
    self.csvHeaders = ko.observableArray([]);
    self.hasHeader = ko.observable(true);
    self.dataSetHeader = ko.observableArray([]);
    self.dataSetResult = ko.observableArray([]);
    self.dataTypeLoading = ko.observable(false);
    self.loadDataTypeUrl = para.loadDataTypeUrl;
    self.uploadFileUrl = para.uploadFileUrl;
    self.convertCSVUrl = para.convertCSVUrl;
    self.currentStep = ko.observable(0);
    self.stepTitle = ko.computed(function () {
        if (self.currentStep() === 0) {
            return self.stepTitleArray.step1;
        }
        if (self.currentStep() === 1) {
            return self.stepTitleArray.step2;
        }
        if (self.currentStep() === 2) {
            return self.stepTitleArray.step3;
        }
    },this);
    // abservable fields defination
    self.configurationItems = ko.observableArray([]);
    // init load for the vm
    self.init = function () {
        self.step.step({
            index1: 0,
            time: 500,
            title: ["step1", "step2", "step3"]
        });
        self.configurationItems.push(GetNewConfigurationItem());
        self.loadDataType();
    };

    self.onfileChanged = function () {
        console.log("onfileChanged", $("#upload")[0].files);
        $("#label").html($("#upload").val());

        var formdata = new FormData();
        formdata.append("file", $("#upload")[0].files[0]);
        $.ajax({
            type: 'post',
            dataType: 'json',
            url: self.uploadFileUrl,
            data: formdata,
            contentType: false,
            processData: false,
            success:(data) => {

            },
             error:(data, timeout, err) => {

             }
        });
    };

    self.loadDataType = function () {
        self.dataTypeLoading(true);
        $.ajax({
            url: self.loadDataTypeUrl,
            success: function (res) {
                self.dataTypeLoading(false);
                if (res.ok) {
                    var data = res.data;
                    self.dataTypeList(data);
                    LogMessage("Load DataType success.", data);
                }
                else {
                    LogMessage("Load DataType failed. (Message:" + res.message + ")");
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                LogMessage("Load DataType failed.");
                self.dataTypeLoading(false);
            }
        });
    };
    // method to add new configuration item
    self.addNewConfiguration = function () {
        self.configurationItems.push(GetNewConfigurationItem());
    };

    // method to delete configuration item
    self.deleteConfiguration = function (item) {
        var leftItems = _.filter(self.configurationItems(), function (conf) {
            var refItem = conf();
            return refItem.id !== item.id;
        });
        self.configurationItems(leftItems);
    };

    self.prevStep = function (index) {
        self.step.prevStep();
        self.currentStep(index);
    };
    // method to go to next step of upload csv file
    self.nextStepToUploadCSV = function () {
        var errorCount = 0;
        _.each(self.configurationItems(), function (item) {
            item.errors.showAllMessages();
            var group = ko.validation.group(item);
            errorCount += group().length;
        });
        if (errorCount > 0) {
            layui.use('layer', function () {
                var layer = layui.layer;
                layer.open({
                    title: 'Message',
                    offset: "rt",
                    shade: 0,
                    time: 2000,
                    btn: [],
                    content: 'Please fill the required fields.'
                });
            }); 
        }
        else {
            self.step.nextStep();
            self.currentStep(1);
            layui.use('upload', function () {
                var upload = layui.upload;
                var uploadInst = upload.render({
                    elem: $('#upload') 
                    , url: self.uploadFileUrl,
                    auto: false,
                    data: {
                        hasHeader: self.hasHeader()
                    },
                    accept:"file",
                    acceptMime: ".csv",
                    exts:"csv",
                    bindAction:"#upload_btn"
                    , choose: function (obj) {
                        obj.preview(function (index, file, result) {
                            $("#file_label").html(file.name);
                        });
                        self.csvHeaders([]);
                    },
                    before: function (obj) {
                        layer.load();
                    }
                    , done: function (res) {
                        layer.closeAll('loading');
                        self.csvHeaders(res.data.Headers);
                        self.csvId(res.data.Id);
                        console.log(res);
                    }
                    , error: function () {
                        layer.closeAll('loading'); 
                        //请求异常回调
                    }
                });
            });
        }
    };

    self.nextStepToDataSetResult = function () {
        var headerJson = convertToJson(self.csvHeaders());
        // validation
      var allNull=  _.every(headerJson, function (item) { return item.Value === null;});
        if (allNull) {
            layui.use('layer', function () {
                var layer = layui.layer;
                layer.open({
                    title: 'Message',
                    offset: "rt",
                    shade: 0,
                    time: 2000,
                    btn: [],
                    content: 'Please map the fileds.'
                });
            });
        }
        else {
            layer.load();
            $.ajax({
                url: self.convertCSVUrl,
                method: "Post",
                data: {
                    Headers: headerJson,
                    Id: self.csvId,
                    HasHeader: self.hasHeader()
                },
                success: function (res) {
                    if (res.ok) {
                        layer.closeAll('loading');
                        self.step.nextStep();
                        self.currentStep(2);
                        var data = res.data;
                        self.dataSetHeader(data.headers);
                        self.dataSetResult(data.rows);
                        LogMessage("Load DataType success.", data);
                    }
                    else {
                        layer.closeAll('loading');
                        LogMessage("Load DataType failed. (Message:" + res.message + ")");
                        layui.use('layer', function () {
                            var layer = layui.layer;
                            layer.open({
                                title: 'Parse Error',
                                btn:["OK"],
                                content: "<div>" + res.message + "</div>" + "<div style='margin-top:10px;'><ul><li><b>Column Index</b>:" + res.error.columnIndex + "</li><li><b>Row Index</b>:" + res.error.rowIndex + "</li><li><b>Row Data</b>:" + res.error.csv +"</li></ul>"
                            });
                        });
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    layer.closeAll('loading');
                    LogMessage("Load DataType failed.");
                }
            });
        }
    };
}



