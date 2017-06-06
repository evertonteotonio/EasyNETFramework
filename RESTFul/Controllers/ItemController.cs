using EFN.Entity.NotMapped;
using EFN.Entity.Stock;
using Microsoft.AspNetCore.Mvc;

//For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace ENF.RESTFul.Controllers
{
    using EFN.Data.Stock;

    using Microsoft.AspNetCore.Authorization;

    [Route("api/[controller]")]
    public class ItemController : BaseController
    {
        /// <summary>
        /// The extended manager.
        /// </summary>
        private readonly ItemExtendedManager extendedManager = new ItemExtendedManager();

        /// <summary>
        /// The find all.
        /// </summary>
        /// <param name="search">
        /// The search.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        [HttpGet]
        [Route("FindAll")]
        public object FindAll(Search search)
        {
            return new { data = DbContext.ItemManager.FindAll(search), count = DbContext.ItemManager.Count() };
        }

        /// <summary>
        /// The find all.
        /// </summary>
        /// <param name="search">
        /// The search.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("ItemWithStock")]
        public object ItemWithStock(Search search)
        {
            return new { data = extendedManager.ItemWithStock(search), count = DbContext.ItemManager.Count() };
        }

        /// <summary>
        /// To get an item by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Item Get(int id)
        {
            return DbContext.ItemManager.FindById(id);
        }
        // POST api/values
        [HttpPost]
        public Item Post([FromBody]Item item)
        {
            return DbContext.ItemManager.Add(item);
        }
        [HttpPut]
        // PUT api/values
        public Item Put([FromBody]Item item)
        {
            return DbContext.ItemManager.Update(item);
        }
        // DELETE api/values
        [HttpDelete]
        public Item Delete([FromBody]Item item)
        {
            return DbContext.ItemManager.Delete(item);
        }
        [Route("Count")]
        [HttpGet]
        public int Count(string where = "")
        {
            where = where != "" ? "where " + where : "";
            return DbContext.ItemManager.Count(where);
        }
    }
}
