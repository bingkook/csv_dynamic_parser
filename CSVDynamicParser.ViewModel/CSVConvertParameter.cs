using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVDynamicParser.ViewModel
{
   public class CSVConvertParameter
    {
        public CSVConvertParameter()
        {
            Headers = new List<HeaderParameter>();
        }
        public bool HasHeader { get; set; }
        public List<HeaderParameter> Headers { get; set; }
        public Guid id { get; set; }
    }

    public class HeaderParameter
    {
        public string Name { get; set; }
        public ConfigurationItemViewModel Value { get; set; }
        public int Index { get; set; }
    }
}
