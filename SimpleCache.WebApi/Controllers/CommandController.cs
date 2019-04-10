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
    }
}
