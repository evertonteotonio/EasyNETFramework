using EFN.Common;
using Dapper.Contrib.Extensions;
using System;

namespace EFN.Entity.Common
{
    public class AuditData
    {
        [Key]
        public int Id { get; set; }
        public string EntityName { get; set; }
        public int EntityId { get; set; }
        public int UserId { get; set; }
        public DateTime ActionTime { get; set; } = DateTime.Now;
        public Enums.ActionType ActionType { get; set; }
    }
}
