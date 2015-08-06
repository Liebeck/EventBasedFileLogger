using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBasedFileLogger
{
    public class LogEventArgs : System.EventArgs
    {
        public LogEventArgs(String message)
        {
            this.Message = message;
        }

        public String Message { get; set; }
    }
}
