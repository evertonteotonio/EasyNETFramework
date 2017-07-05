using ENF.Entity.Orders;
using GraphQL.Types;

namespace ENF.RESTFul.Helpers
{
    public class OrdersQuery : ObjectGraphType
    {
        public OrdersQuery()
        {
            Field<OrderType>(
              "test",
              resolve: context => new Order { Id = 1, CustomerName  = "R2-D2", Amount = 151 }
            );
        }
    }

    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType()
        {
            Field(x => x.Id, nullable: true).Description("The Id of the Droid.");
            Field(x => x.CustomerName, nullable: true).Description("The name of the Droid.");
            Field(x => x.Amount, nullable: true).Description("The amount of the Droid.");
        }
    }
}
