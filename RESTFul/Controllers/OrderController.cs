using System.Collections.Generic;
using ENF.Entity.NotMapped;
using ENF.Entity.Orders;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ENF.RESTFul.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : BaseController
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpGet]
        [Route("FindAll")]
        public List<Order> FindAll(Search search)
        {
            return DbContext.OrderManager.FindAll(search);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Order Get(int id)
        {
            return DbContext.OrderManager.FindById(id);
        }

        // POST api/values
        [HttpPost]
        public Order Post([FromBody]Order Order)
        {
            return DbContext.OrderManager.Add(Order);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public Order Put([FromQuery]Order Order)
        {
            return DbContext.OrderManager.Update(Order);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public Order Delete([FromBody]Order order)
        {
            return DbContext.OrderManager.Delete(order);
        }
    }
}
