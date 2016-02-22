using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Diagnostics;
using System.IO;

namespace MyFixIt.Logging
{

    public enum LogLevel
    {
        Debug,
        Info,
        Warn,
        Error,
        Fatal,
        ItemPricing
    }

    public class LoggerLog4Net : ILogger
    {
        public  readonly ILog log = LogManager.GetLogger(typeof(LoggerLog4Net));
        public  LoggerLog4Net()
        {
            log4net.Config.XmlConfigurator.Configure();
        }


        public void Information(string message)
        {

        }
        public void Information(string fmt, params object[] vars)
        {

        }
        public void Information(Exception exception, string fmt, params object[] vars)
        {

        }

        public void Warning(string message)
        {

        }
        public void Warning(string fmt, params object[] vars)
        {

        }
        public void Warning(Exception exception, string fmt, params object[] vars)
        {

        }

        public void Error(string message)
        {
            log.Error(message);
        }
        public void Error(string fmt, params object[] vars)
        {
            Error(String.Format(fmt, vars));
        }
        public void Error(Exception exception, string fmt, params object[] vars)
        {
            var msg = String.Format(fmt, vars);
            msg += exception.Message;
            Error(msg);
            
        }

        //
        // TraceAPI - trace inter-service calls (including latency)

        public void TraceApi(string componentName, string method, TimeSpan timespan)
        {
            TraceApi(componentName, method, timespan, "");
        }

        public void TraceApi(string componentName, string method, TimeSpan timespan, string fmt, params object[] vars)
        {
            TraceApi(componentName, method, timespan, string.Format(fmt, vars));
        }

        public void TraceApi(string componentName, string method, TimeSpan timespan, string properties)
        {
            string message = String.Concat("component:", componentName, ";method:", method, ";timespan:", timespan.ToString(), ";properties:", properties);
            Trace.TraceInformation(message);
        }
    }
}
