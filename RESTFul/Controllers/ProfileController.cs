using Data;
using Entity;
using Entity.NotMapped;
using Microsoft.AspNetCore.Mvc;
using RESTFul.CacheManager;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTFul.Controllers
{
    //[EnableCors("Main")]
    [Route("api/[controller]")]
    public class ProfileController : Controller
    {
        ProfileManager manager = new ProfileManager();
        // GET: api/values
        [HttpGet]
        [Route("FindAll")]
        public object FindAll(Search search)
        {
            string cacheName = $"Profile-{search.page}-{search.limit}-{search.query}-{search.orderBy}";
            var cached = CacheDataManager<object>.GetItem(cacheName);
            if (cached == null)
            {
                cached = new {data = manager.FindAll(search), count = manager.Count()};
                CacheDataManager<object>.AddItem(cacheName, cached); 
            }
            return cached;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Profile Get(int id)
        {
            return manager.FindById(id);
        }

        // POST api/values
        [HttpPost]
        public Profile Post([FromBody]Profile profile)
        {
            return manager.Add(profile);
        }
        [HttpPut]
        // PUT api/values
        public Profile Put([FromBody]Profile profile)
        {
            return manager.Update(profile);
        }

        // DELETE api/values
        [HttpDelete]
        public Profile Delete([FromBody]Profile profile)
        {
            return manager.Delete(profile);
        }
        [Route("Count")]
        [HttpGet]
        public int Count(string where = "")
        {
            return manager.Count(where);
        }
    }
}
