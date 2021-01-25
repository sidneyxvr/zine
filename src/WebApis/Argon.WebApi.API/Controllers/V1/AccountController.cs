using Argon.Core.Communication;
using Argon.Identity.Application.Commands;
using Argon.WebApi.API.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Argon.WebApi.API.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IBus _bus;

        public AccountController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(UserRequest request)
        {
            var result = await _bus.SendAsync(new CreateUserCommand(
                request.FullName, request.Email, request.Phone, 
                request.Cpf, request.BirthDate, request.Gender, request.Password));

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}
