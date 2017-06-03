using Dapper.Contrib.Extensions;

namespace EFN.Entity.Common
{
    public class Address
    {
        [Key]
        public int? Id { get; set; }
        public string LocationName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
    }
}
