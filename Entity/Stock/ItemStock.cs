using Dapper.Contrib.Extensions;

namespace ENF.Entity.Stock
{
    public class ItemStock
    {
        [Key]
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int? Available { get; set; } = 0;
        public int? Sold { get; set; } = 0;
        public int? Reserved { get; set; } = 0;
        public int? WarehouseId { get; set; }
    }
}
