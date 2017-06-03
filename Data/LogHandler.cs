using System;
using Common;
using Entity.System;

namespace Data
{
    public static class LogHandler
    {
        public static void Error(Exception ex, string msg = "")
        {
            Context context = new Context();
            context.LogManager.Add(new Log
            {
                Exception = ex.Message,
                Data = ex.StackTrace,
                Message = msg,
                Level = Enums.LogLevel.Error
            });
        }
        public static void Info(string msg,object value = null)
        {
            Context context = new Context();
            context.LogManager.Add(new Log
            {
                Data = value.ToString(),
                Message = msg,
                Level = Enums.LogLevel.Info
            });
        }
        public static void Trace(string msg, object value = null)
        {
            Context context = new Context();
            context.LogManager.Add(new Log
            {
                Data = value.ToString(),
                Message = msg,
                Level = Enums.LogLevel.Trace
            });
        }
        public static void Debug(string msg, object value)
        {
            Context context = new Context();
            context.LogManager.Add(new Log
            {
                Data = value.ToString(),
                Message = msg,
                Level = Enums.LogLevel.Debug
            });
        }
    }
}
