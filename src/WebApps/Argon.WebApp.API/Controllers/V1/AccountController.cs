using Argon.Identity.Requests;
using Argon.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Argon.WebApp.API.Controllers.V1
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

        [HttpPost("register-supplier")]
        public async Task<IActionResult> CreateSupplierUserAsync(SupplierUserRequest request)
        {
            var result = await _accountService.CreateSupplierUserAsync(request);

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
