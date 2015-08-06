using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBasedFileLogger
{
    public class Logger
    {

        public event EventHandler<LogEventArgs> LogChanged;

        internal virtual void OnLog(String message)
        {
            EventHandler<LogEventArgs> handler = this.LogChanged;
            if (handler != null)
            {
                handler(this, new LogEventArgs(message));
            }
        }

        public static void Log(String text, String path)
        {
            text = String.Format("{0}: {1}", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"), text);
            System.IO.File.AppendAllText(path, text + Environment.NewLine);
        }

        public static String GetAllExceptionMessages(Exception ex, String text)
        {
            Exception exceptionReference = ex;
            StringBuilder stringBuilder = new StringBuilder();
            String exceptionName = ex.GetType().Name;
            if (!String.IsNullOrEmpty(text))
            {
                stringBuilder.Append(text);
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append("Error message:");
                stringBuilder.Append(Environment.NewLine);
            }
            stringBuilder.AppendFormat("{0}: {1}", exceptionName, exceptionReference.Message);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(ex.StackTrace);
            while (exceptionReference.InnerException != null)
            {
                exceptionReference = exceptionReference.InnerException;
                exceptionName = exceptionReference.GetType().Name;
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append("-------------------------------------------------------");
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.AppendFormat("{0}: {1}", exceptionName, exceptionReference.Message);
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append(exceptionReference.StackTrace);
            }
            return stringBuilder.ToString();
        }
    }
}
