using CSVDynamicParser.Core;
using CSVDynamicParser.Servies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVDynamicParser.Tests.ServiceTest
{
   public  class ServiceTestBase
    {
        public ICommonService CommonService
        {
            get
            {
                return InjectContainer.GetInstance<ICommonService>();
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
