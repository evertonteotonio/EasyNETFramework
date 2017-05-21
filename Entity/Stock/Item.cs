using System;
using Dapper.Contrib.Extensions;

namespace Entity.Stock
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int Qty { get; set; } = 0;
        public float Price { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
