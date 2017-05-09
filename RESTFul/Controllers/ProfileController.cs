using System.Collections.Generic;
using Data;
using Entity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTFul.Controllers
{
    [Route("api/[controller]")]
    public class ProfileController : Controller
    {
        // GET: api/values
        [HttpGet]
        [Route("FindAll")]
        public List<Profile> FindAll(int page = 1,
            int pageSize = 10,
            string where = "",
            string orderBy = "")
        {
            ProfileManager manager = new ProfileManager();
            return manager.FindAll(page, pageSize, where, orderBy);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Profile Get(int id)
        {
            ProfileManager manager = new ProfileManager();
            return manager.FindById(id);
        }

        // POST api/values
        [HttpPost]
        public Profile Post(Profile profile)
        {
            ProfileManager manager = new ProfileManager();
            return manager.Add(profile);
        }

        // PUT api/values
        public Profile Put(Profile profile)
        {
            ProfileManager manager = new ProfileManager();
            return manager.Update(profile);
        }

        // DELETE api/values
        [HttpDelete]
        public Profile Delete(Profile profile)
        {
            ProfileManager manager = new ProfileManager();
            return manager.Delete(profile);
        }
        [Route("Count")]
        [HttpGet]
        public int Count(string where = "")
        {
            ProfileManager manager = new ProfileManager();
            return manager.Count(where);
        }
    }
}
