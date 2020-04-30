using CSVDynamicParser.Common;
using CSVDynamicParser.Common.Exceptions;
using CSVDynamicParser.Servies.Interfaces;
using CSVDynamicParser.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSVDynamicParser.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            var iss = CommonService.getDataTypes();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region  Get DataType List
        /// <summary>
        /// ajax method to get data type defination list
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDataTypeList()
        {
            var result = new ResultData<List<EnumItem>>()
            {
                data = new List<EnumItem>()
            };
            try
            {
                var list = CommonService.getDataTypes();
                result.ok = true;
                result.data = list;
            }
            catch (Exception e)
            {
                result.ok = false;
                result.message = e.Message;
                LogHelper.LogException("GetDataTypeList Failed", e);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion
        public JsonResult UploadCSVFile(bool hasHeader)
        {
            var result = new ResultData<UploadedFile>()
            {
                data = new UploadedFile()
            };
            try
            {
                var files = Request.Files;

                if (files.Count == 0)
                {
                    result.ok = false;
                    result.message = "no file received";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                var headers = FileService.ReadCSVHeader(hasHeader, files[0]);

                result.ok = true;
                result.data = headers;
            }
            catch (Exception e)
            {
                result.ok = false;
                result.message = e.Message;
                LogHelper.LogException("UploadCSVFile Failed", e);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ConvertCSVToDatatable(CSVConvertParameter para)
        {
            var result = new ResultData<DataTableViewModel>()
            {
                data = new DataTableViewModel()
            };
            try
            {
                if (para == null)
                {
                    throw new Exception("Parameter error.");
                }
                var dt = FileService.ConvertCSVResult(para);
                var res = FileService.GetDataTableResult(dt);
                result.data = res;
                result.ok = true;
            }
            catch (ParseException e)
            {
                result.ok = false;
                result.message = e.Message;
                result.error = e.GetData();
                LogHelper.LogException("UploadCSVFile Failed", e);
            }
            catch (Exception e)
            {
                result.ok = false;
                result.error = new ParseExceptionParameter()
                {
                    columnIndex=0,
                    rowIndex=0
                };
                result.message = e.Message;
                LogHelper.LogException("UploadCSVFile Failed", e);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}