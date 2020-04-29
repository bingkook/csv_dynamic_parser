"use strict";
// generate unique guid
function uuid() {
    var s = [];
    var hexDigits = "0123456789abcdefghijklmnopqistuvwxyz";
    for (var i = 0; i < 36; i++) {
        s[i] = hexDigits.substr(Math.floor(Math.random() * 0x10), 1);
    }
    s[14] = "4";  // bits 12-15 of the time_hi_and_version field to 0010
    s[19] = hexDigits.substr((s[19] & 0x3) | 0x8, 1);  // bits 6-7 of the clock_seq_hi_and_reserved to 01
    s[8] = s[13] = s[18] = s[23] = "-";

    var uuid = s.join("");
    return uuid;
}

// convert list to observable obj
function convertToObservable(list) {
    var newList = [];
    $.each(list, function (i, obj) {
        var newObj = {};
        Object.keys(obj).forEach(function (key) {
            newObj[key] = ko.observable(obj[key]);
        });
        newList.push(newObj);
    });
    return newList;
}

function convertToJson(list) {
    var newList = [];
    $.each(list, function (i, item) {
        var observable = item.Value;
        var value = null;
        if (observable) {
            var name = observable.name();
            var dataType = observable.dataType();
            var required = observable.required();
            var size = observable.size();
            value = {
                id: observable.id,
                name: name,
                dataType: dataType,
                required: required,
                size: size
            };
        }
        var newObj = {
            Name: item.Name,
            Index:item.Index,
            Value: value
        };
        newList.push(newObj);
    });
    return newList;
}

// log message to console
function LogMessage(message1, message2, message3) {
    console.log(message1, message2, message3);
}