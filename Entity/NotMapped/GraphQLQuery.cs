
namespace ENF.Entity.NotMapped
{
    public class GraphQLQuery
    {
        public string OperationName { get; set; }
        public string NamedQuery { get; set; }
        public string query { get; set; }
        public string Variables { get; set; }
    }
}
