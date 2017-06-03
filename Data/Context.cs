<<<<<<< HEAD
﻿using EFN.Entity;
using EFN.Entity.Stock;
=======
﻿using Entity;
using Entity.Stock;
using Entity.System;
>>>>>>> origin/master

namespace EFN.Data
{
    public class Context
    {
       public GeneralManager<Profile> ProfileManager = new GeneralManager<Profile>();
       public GeneralManager<User> UserManager = new GeneralManager<User>();
       public GeneralManager<Item> ItemManager = new GeneralManager<Item>();
       public GeneralManager<ItemStock> ItemStockManager = new GeneralManager<ItemStock>();
       public GeneralManager<Warehouse> WarehouseManager = new GeneralManager<Warehouse>();
       public GeneralManager<Log> LogManager = new GeneralManager<Log>();
    }
}
