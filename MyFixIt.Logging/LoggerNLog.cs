using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using NLog;

namespace MyFixIt.Logging
{
    public class LoggerNLog : ILogger
    {
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
        //LogManager.ThrowExceptions = true;
        //private static NLog.Logger logger = LogManager.GetLogger("foo");

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
            logger.Error(message);
        }
        public void Error(string fmt, params object[] vars)
        {
            logger.Error(String.Format(fmt, vars));
        }
        public void Error(Exception exception, string fmt, params object[] vars)
        {
            var msg = String.Format(fmt, vars);
            msg += exception.Message;
            logger.Error(msg);
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
