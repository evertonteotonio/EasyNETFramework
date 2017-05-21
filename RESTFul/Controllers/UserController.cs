using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Data;
using Entity;
using Entity.NotMapped;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTFul.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        UserManager manager = new UserManager();
        // GET: api/values
        //[JwtAuthentication]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        [HttpGet("FindbyName/{UserName}")]
        public User GetByUserName([FromRoute]string UserName)
        {
            return manager.FindByUserName(UserName);
        }

        // POST api/values
        [HttpPost]
        public User Post([FromBody]User user)
        {
            user.Password = user.sha256(user.Password);
            return manager.Add(user);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        [Route("Count")]
        [HttpGet]
        public int Count(string where = "")
        {
            return manager.Count(where);
        }
    }
}
