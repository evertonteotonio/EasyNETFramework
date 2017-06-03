using Dapper.Contrib.Extensions;

namespace EFN.Entity.Stock
{
    public class Item
    {
        [Key]
        public int? Id { get; set; }
        public string ItemName { get; set; }
        public float Price { get; set; } = 0;
        public bool IsActive { get; set; }
        public bool IsDownload { get; set; }
        public bool IsDeleted { get; set; }
    }
}
