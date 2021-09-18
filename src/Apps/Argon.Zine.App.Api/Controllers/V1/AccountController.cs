using Argon.Zine.Identity.Requests;
using Argon.Zine.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Argon.Zine.App.Api.Controllers.V1
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

        [HttpPost("register-customer")]
        public async Task<IActionResult> CreateCustomerUserAsync(CustomerUserRequest request)
        {
            var result = await _accountService.CreateCustomerUserAsync(request);

            return CustomResponse(result);
        }

        [HttpPost("register-restaurant")]
        public async Task<IActionResult> CreateSupplierUserAsync(RestaurantUserRequest request)
        {
            var result = await _accountService.CreateRestaurantUserAsync(request);

            return CustomResponse(result);
        }

        [HttpPost("email-confirmation")]
        public async Task<IActionResult> EmailConfirmationAsync(EmailAccountConfirmationRequest request)
        {
            var result = await _accountService.ConfirmEmailAccountAsync(request);

            return CustomResponse(result);
        }

        [HttpPost("resend-email-confirmation")]
        public async Task<IActionResult> ResendEmailConfirmationAsync(EmailRequest request)
        {
            var result = await _accountService.ResendConfirmEmailAccountAsync(request);

            return CustomResponse(result);
        }

        [HttpPost("send-reset-password")]
        public async Task<IActionResult> SendResetPasswordAsync(EmailRequest request)
        {
            var result = await _accountService.SendResetPasswordAsync(request);

            return CustomResponse(result);
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var result = await _accountService.ResetPasswordAsync(request);

            return CustomResponse(result);
        }
    }
}
