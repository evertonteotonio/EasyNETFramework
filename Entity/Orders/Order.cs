using Dapper.Contrib.Extensions;

namespace ENF.Entity.Orders
{
    public class Order
    {
        [Key]
        public int? Id { get; set; }
        public string OrderTitle { get; set; }
        public decimal Amount { get; set; }
        public string CustomerName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
