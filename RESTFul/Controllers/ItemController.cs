using Data.Stock;
using Entity.NotMapped;
using Entity.Stock;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTFul.Controllers
{
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        readonly ItemManager _manager = new ItemManager();
        // GET: api/values
        [HttpGet]
        [Route("FindAll")]
        public object FindAll(Search search)
        {
            return new { data = _manager.FindAll(search), count = _manager.Count() };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Item Get(int id)
        {
            return _manager.FindById(id);
        }

        // POST api/values
        [HttpPost]
        public Item Post([FromBody]Item item)
        {
            return _manager.Add(item);
        }
        [HttpPut]
        // PUT api/values
        public Item Put([FromBody]Item item)
        {
            return _manager.Update(item);
        }

        // DELETE api/values
        [HttpDelete]
        public Item Delete([FromBody]Item item)
        {
            return _manager.Delete(item);
        }
        [Route("Count")]
        [HttpGet]
        public int Count(string where = "")
        {
            return _manager.Count(where);
        }
    }
}
