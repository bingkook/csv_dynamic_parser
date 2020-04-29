using CSVDynamicParser.Core;
using CSVDynamicParser.Servies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSVDynamicParser.Web.Controllers
{
    public class ControllerBase : Controller
    {
        public ICommonService CommonService
        {
            get
            {
                return InjectContainer.GetInstance<ICommonService>();
            }
        }

        public ILogHelper LogHelper
        {
            get
            {
                return InjectContainer.GetInstance<ILogHelper>();
            }
        }

        public IFileService FileService
        {
            get
            {
                return InjectContainer.GetInstance<IFileService>();
            }
        }
    }
}