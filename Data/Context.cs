using Entity;
using Entity.Stock;

namespace Data
{
    public class Context
    {
       public GeneralManager<Profile> ProfileManager = new GeneralManager<Profile>();
       public GeneralManager<User> UserManager = new GeneralManager<User>();
       public GeneralManager<Item> ItemManager = new GeneralManager<Item>();
    }
}
