using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Argon.Zine.App.Api.Controllers.V2;

[ApiController]
[AllowAnonymous]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/ping")]
public class PingController : ControllerBase
{
    [HttpGet]

    public IActionResult Ping()
        => Ok("V2");
}