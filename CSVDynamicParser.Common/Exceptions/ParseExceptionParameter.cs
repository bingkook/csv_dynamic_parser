using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVDynamicParser.Common.Exceptions
{
  public class ParseExceptionParameter
    {
        public CsvRow csv { get; set; }
        public int columnIndex { get; set; }
        public int rowIndex { get; set; }
    }
}
