using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVDynamicParser.ViewModel
{
  public  class UploadedFile
    {
        public UploadedFile()
        {
            Headers = new List<HeaderItem>();
        }
        public Guid Id { get; set; }

        public List<HeaderItem> Headers { get; set; }
    }

    public class HeaderItem
    {
        public int Index { get; set; }
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
