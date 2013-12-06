using System.Collections.Generic;
using System.Web.Http;

namespace Waste.Web.Api.Controllers
{
    public class MenuController : ApiController
    {
        // GET api/menu
        public IEnumerable<Product> Get()
        {
            return new[] { new Product() { Id = 1, Name = "Mocha", Description = "Strong coffee with a shot of chocolate" } };
        }

        // GET api/menu/5
        public Product Get(int id)
        {
            return new Product() { Id = 1, Name = "Mocha", Description = "Strong coffee with a shot of chocolate" };
        }

        // POST api/menu
        public void Post([FromBody]string value)
        {

        }

        // PUT api/menu/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/menu/5
        public void Delete(int id)
        {

        }
    }
}
