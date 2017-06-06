using System;
using ENF.Common;

namespace Entity.System
{
    public class Log
    {
        public int? Id { get; set; }
        public DateTime LoggedAt { get; set; } = DateTime.Now;
        public string Message { get; set; }
        public string Data { get; set; } 
        public Enums.LogLevel LogLevel { get; set; }
        public string Exception { get; set; }
        public string UserData { get; set; }
    }
}
