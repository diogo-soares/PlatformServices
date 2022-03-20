using System;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.controller
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {

        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Post Comands ok deu certo mano");

            return Ok("platform test of ");
        }
    }
}