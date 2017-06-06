using ENF.Entity.Stock;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
namespace ENF.Data.Stock
{
    using ENF.Entity.NotMapped;

    public class ItemExtendedManager : GeneralManager<Item>
    {
        public List<Item> ItemWithStock(Search search)
        {
            using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
            {
                var items = FindAll(search);
                var itemStock = connection.Query<ItemStock>("SELECT * FROM dbo.ItemStock [is] JOIN dbo.Item i ON [is].Id = i.Id");
                foreach (var item in items)
                {
                    item.ItemStock =(ItemStock)itemStock.FirstOrDefault(stock => stock.ItemId == item.Id);
                }
                return items;
            }
        }
    }
}
