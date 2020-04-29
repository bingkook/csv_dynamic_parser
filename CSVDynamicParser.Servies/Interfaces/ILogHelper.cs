using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVDynamicParser.Servies.Interfaces
{
    public interface ILogHelper
    {
        void LogInfo(string info);
        void LogException(string info, Exception ex);
        void LogError(string info);
    }
}
