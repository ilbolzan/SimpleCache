using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleCache.CLI;

namespace SimpleCache.WebApi.Controllers
{
    [Route("/")]
    [ApiController]
    public class CommandController : ControllerBase
    {

        [HttpGet]
        public ActionResult<string> Get([FromQuery] string cmd)
        {
            return CLIParser.Cmd(cmd);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
