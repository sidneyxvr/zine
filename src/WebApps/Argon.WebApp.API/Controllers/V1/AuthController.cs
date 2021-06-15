﻿using Argon.Identity.Requests;
using Argon.Identity.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Argon.WebApp.API.Controllers.V1
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);

            return CustomResponse(response);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var response = await _authService.RefreshTokenAsync(request);

            return CustomResponse(response);
        }
    }
}
