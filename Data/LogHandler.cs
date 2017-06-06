using System;
using ENF.Common;
using Entity.System;
using System.Data.SqlClient;
using Dapper;
namespace ENF.Data
{
    public static class LogHandler
    {
        public static void Error(Exception ex, string msg = "")
        {
            using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
            {
                connection.Insert(new Log
                {
                    Exception = ex.Message,
                    Data = ex.StackTrace,
                    Message = msg,
                    LogLevel = Enums.LogLevel.Error
                });
            }
        }
        public static void Info(string msg, object value = null)
        {
            using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
            {
                connection.Insert(new Log
                {
                    Data = value?.ToString(),
                    Message = msg,
                    LogLevel = Enums.LogLevel.Info
                });
            }

        }
        public static void Trace(string msg, object value = null)
        {
            using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
            {
                connection.Insert(new Log
                {
                    Data = value?.ToString(),
                    Message = msg,
                    LogLevel = Enums.LogLevel.Trace
                });
            }
        }
        public static void Debug(string msg, object value)
        {
            using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
            {
                connection.Insert(new Log
                {
                    Data = value?.ToString(),
                    Message = msg,
                    LogLevel = Enums.LogLevel.Debug
                });
            }
        }
    }
}
