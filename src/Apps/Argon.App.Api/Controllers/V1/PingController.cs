using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Argon.App.Api.Controllers.V1
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/ping")]
    public class PingController : ControllerBase
    {
        [HttpGet]

        public IActionResult Ping()
            => Ok("V1");
    }
}
