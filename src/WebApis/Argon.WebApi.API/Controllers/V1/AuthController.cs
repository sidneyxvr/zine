﻿using Argon.Identity.Requests;
using Argon.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Argon.WebApi.API.Controllers.V1
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
        public async Task<IActionResult> LoginInAsync(LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);

            return CustomResponse(response);
        }
    }
}