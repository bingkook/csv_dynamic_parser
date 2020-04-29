using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVDynamicParser.Common.Exceptions
{
    public class ParseException : ApplicationException
    {
        private string error;
        private ParseExceptionParameter data;
        private Exception innerException;
        public ParseException()
        {

        }
        public ParseException(string msg) : base(msg)
        {
            this.error = msg;
        }
        public ParseException(string msg, ParseExceptionParameter para) : base(msg)
        {
            this.error = msg;
            this.data = para;
        }
        public ParseException(string msg, Exception innerException) : base(msg)
        {
            this.innerException = innerException;
            this.error = msg;
        }
        public ParseException(string msg, Exception innerException, ParseExceptionParameter para) : base(msg)
        {
            this.innerException = innerException;
            this.data = para;
            this.error = msg;
        }
        public string GetError()
        {
            return error;
        }
        public ParseExceptionParameter GetData()
        {
            return data;
        }
    }
}
