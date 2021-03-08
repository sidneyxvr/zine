﻿using Argon.Identity.Requests;
using Argon.Identity.Services;
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
        public async Task<IActionResult> CreateCustomerUserAsync(CustomerUserRequest request)
        {
            var result = await _accountService.CreateCustomerUserAsync(request);

            return CustomResponse(result);
        }

        [HttpPost("email-confirmation")]
        public async Task<IActionResult> EmailConfirmationAsync(AccountEmailConfirmationRequest request)
        {
            var result = await _accountService.ConfirmAccountEmailAsync(request);

            return CustomResponse(result);
        }

        [HttpPost("resend-email-confirmation")]
        public async Task<IActionResult> ResendEmailConfirmationAsync(EmailRequest request)
        {
            var result = await _accountService.ResendConfirmAccountEmailAsync(request);

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

        [HttpPut("two-factory-authentication")]
        public async Task<IActionResult> UpdateTwoFactoryAuthenticationAsync(UpdateTwoFactorAuthenticationRequest request)
        {
            var result = await _accountService.UpdateTwoFactorAuthenticationAsync(request);

            return CustomResponse(result);
        }
    }
}
