using CSVDynamicParser.Common;
using CSVDynamicParser.Servies.Interfaces;
using CSVDynamicParser.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVDynamicParser.Servies.Implements
{
    public class CommonService : ICommonService
    {
        public List<EnumItem> getDataTypes()
        {
            return EnumHelper.Enum2List<DataTypeEnum>();
        }
    }
}
