
namespace EFN.Entity.NotMapped
{
    public class Search
    {
        public int page { get; set; } = 1;
        public int limit { get; set; } = 10;
        public string orderBy { get; set; }
        public string query { get; set; }
    }
}
