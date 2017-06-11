using ENF.Entity;
using ENF.Entity.Stock;
using Entity.System;
using ENF.Entity.Orders;

namespace ENF.Data
{
    public class Context
    {
       public GeneralManager<Profile> ProfileManager = new GeneralManager<Profile>();
       public GeneralManager<User> UserManager = new GeneralManager<User>();
       public GeneralManager<Item> ItemManager = new GeneralManager<Item>();
       public GeneralManager<ItemStock> ItemStockManager = new GeneralManager<ItemStock>();
       public GeneralManager<Warehouse> WarehouseManager = new GeneralManager<Warehouse>();
       public GeneralManager<Log> LogManager = new GeneralManager<Log>();
       public GeneralManager<Order> OrderManager = new GeneralManager<Order>();
    }
}
