using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVDynamicParser.ViewModel
{
  public  class DataTableViewModel
    {
        public DataTableViewModel()
        {
            headers = new List<string>();
            rows = new List<DataRowViewModel>();
        }
        public List<string> headers { get; set; }
        public List<DataRowViewModel> rows { get; set; }
    }
    public class DataRowViewModel
    {
        public DataRowViewModel()
        {
            values = new List<string>();
        }
        public List<string> values { get; set; }
    }
}
