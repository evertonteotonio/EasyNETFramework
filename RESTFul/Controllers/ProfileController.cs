using System.Collections.Generic;
using Data;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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
        public object FindAll(int page = 1,
            int limit = 10,
            string query = "",
            string orderBy = "")
        {
            return new { data = manager.FindAll(page, limit, query, orderBy), count = manager.Count() };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Profile Get(int id)
        {
            return manager.FindById(id);
        }

        // POST api/values
        [HttpPost]
        public Profile Post(Profile profile)
        {
            return manager.Add(profile);
        }

        // PUT api/values
        public Profile Put(Profile profile)
        {
            return manager.Update(profile);
        }

        // DELETE api/values
        [HttpDelete]
        public Profile Delete(Profile profile)
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
