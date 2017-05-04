using System;
using Dapper.Contrib.Extensions;

namespace Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public int? ProfileId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
