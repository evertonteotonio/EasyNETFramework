using Data;
using Entity;
using Entity.NotMapped;
using LazyCache;
using Microsoft.AspNetCore.Mvc;
using System;
using RESTFul.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTFul.Controllers
{
    //[EnableCors("Main")]
    [Route("api/[controller]")]
    public class ProfileController : BaseController
    {
        ProfileManager manager = new ProfileManager();
        // GET: api/values
        [HttpGet]
        [Route("FindAll")]
        public object FindAll(Search search)
        {
            IAppCache cache = new CachingService();
            string cacheName = $"Profile-{search.page}-{search.limit}-{search.query}-{search.orderBy}";
            var products = cache.GetOrAdd(cacheName,
                () => new { data = manager.FindAll(search), count = manager.Count() },
                DateTimeOffset.Now.AddMinutes(5));
            return products;
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
        [Log]
        [Route("Count")]
        [HttpGet]
        public int Count(string where = "")
        {
            return manager.Count(where);
        }
    }
}
