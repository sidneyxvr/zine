using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Argon.WebApi.API.Controllers
{
    [Route("api/v1/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            await Task.Delay(500);

            return Ok("Ok");
        }
    }
}
