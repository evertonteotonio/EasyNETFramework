using Dapper.Contrib.Extensions;

namespace EFN.Entity.Stock
{
    public class Warehouse
    {
        [Key]
        public int? Id { get; set; }
        public string WarehouseName { get; set; }
        public int? AddressId { get; set; }
        public string WorkingHours { get; set; }
        public string WorkingDays { get; set; }
    }
}
