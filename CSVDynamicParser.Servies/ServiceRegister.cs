using CSVDynamicParser.Core;
using CSVDynamicParser.Servies.Implements;
using CSVDynamicParser.Servies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVDynamicParser.Servies
{
  public  class ServiceRegister
    {
        public static void Register()
        {
            InjectContainer.RegisterType<ICommonService, CommonService>();

            InjectContainer.RegisterType<ILogHelper, LogHelper>();

            InjectContainer.RegisterType<IFileService, FileService>();
        }
    }
}
