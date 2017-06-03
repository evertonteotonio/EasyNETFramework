using System;
using EFN.Common;
using EFN.Data;
using Entity.System;

namespace EFN.Data
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
                LogLevel = Enums.LogLevel.Error
            });
        }
        public static void Info(string msg,object value = null)
        {
            Context context = new Context();
            context.LogManager.Add(new Log
            {
                Data = value.ToString(),
                Message = msg,
                LogLevel = Enums.LogLevel.Info
            });
        }
        public static void Trace(string msg, object value = null)
        {
            Context context = new Context();
            context.LogManager.Add(new Log
            {
                Data = value.ToString(),
                Message = msg,
                LogLevel = Enums.LogLevel.Trace
            });
        }
        public static void Debug(string msg, object value)
        {
            Context context = new Context();
            context.LogManager.Add(new Log
            {
                Data = value.ToString(),
                Message = msg,
                LogLevel = Enums.LogLevel.Debug
            });
        }
    }
}
