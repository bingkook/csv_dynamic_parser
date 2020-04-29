using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVDynamicParser.ViewModel
{
  public class ConfigurationItemViewModel
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public DataTypeEnum dataType { get; set; }
        public int size { get; set; }
        public bool required { get; set; }
    }
}
