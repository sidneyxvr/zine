using Argon.Core.Communication;
using Argon.Identity.Application.Commands;
using Argon.WebApi.API.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Argon.WebApi.API.Controllers.V1
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : BaseController
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
                request.FirstName, request.Surname, request.Email, request.Phone, 
                request.Cpf, request.BirthDate, request.Gender, request.Password));

            return CustomResponse(result);
        }
    }
}
