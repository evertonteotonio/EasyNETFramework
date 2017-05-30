using System;
using NLog;
namespace Common
{
    public static class LogHandler
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public static void Error(Exception ex, string msg = "")
        {
            /*Logger.Error(ex, msg);*/
        }
        public static void Info(string msg,object value = null)
        {
            /*Logger.Info(msg);
            if (value != null)
                Logger.Info(value);*/
        }
        public static void Trace(string msg, object value = null)
        {
           /* Logger.Trace(msg);
            if (value != null)
                Logger.Trace(value);*/
        }
        public static void Debug(string msg, object value)
        {
            /*Logger.Debug(msg);
            if (value != null)
                Logger.Debug(value);*/
        }
    }
}
