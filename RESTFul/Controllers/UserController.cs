using System.Collections.Generic;
using Data;
using Microsoft.AspNetCore.Mvc;
using Entity;
using RESTFul.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTFul.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        UserExtendedManager _extendedManager = new UserExtendedManager();
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
            return _extendedManager.FindByUserName(UserName);
        }
        [HttpGet("FindbyId/{Id}")]
        public User GetById([FromRoute]int Id)
        {
            return _extendedManager.FindById(Id);
        }
        // POST api/values
        [HttpPost]
        public User Post([FromBody]User user)
        {
            user.Password = user.sha256(user.Password);
            return _extendedManager.Add(user);
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
            return _extendedManager.Count(where);
        }
    }
}
