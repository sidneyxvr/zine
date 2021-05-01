using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Argon.WebApp.API.Controllers.V2
{
    [ApiController]
    [AllowAnonymous]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/ping")]
    public class PingController : ControllerBase
    {
        [HttpGet]
        
        public IActionResult Ping()
        {
            return Ok("V2");
        }
    }
}
