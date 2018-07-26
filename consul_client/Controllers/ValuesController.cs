using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using Consul4NetCore;
using Microsoft.AspNetCore.Mvc;

namespace consul_client.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private const string serviceName = "netcore-consul";
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            ConsulHttpClient client = new ConsulHttpClient();
            string value = client.DoGet(serviceName, "api/Health");
            return new string[] { value };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
    }
}
