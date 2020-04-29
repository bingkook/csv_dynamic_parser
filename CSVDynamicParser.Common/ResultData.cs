using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVDynamicParser.Common
{
    public class ResultData<T>
    {
        public bool ok { get; set; }

        public T data { get; set; }

        public object error { get; set; }
        public string message { get; set; }
    }
}
