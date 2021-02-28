using Argon.Identity.Application.Models;
using Argon.Identity.Application.Services;
using Argon.WebApi.API.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Argon.WebApi.API.Controllers.V1
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(UserModel request)
        {
            var result = await _accountService.CreateCustomerUserAsync(new CustomerUserRequest(
                request.FirstName, request.Surname, request.Email, request.Phone, 
                request.Cpf, request.BirthDate, request.Gender, request.Password));

            return CustomResponse(result);
        }
    }
}
