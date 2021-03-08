using Argon.Identity.Requests;
using Argon.Identity.Responses;
using Argon.Identity.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Argon.Identity.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly SignInManager<IdentityUser<Guid>> _signInManager;

        public AuthService(
            ITokenService tokenService,
            UserManager<IdentityUser<Guid>> userManager,
            SignInManager<IdentityUser<Guid>> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        public async Task<IdentityResponse<UserLoginResponse>> LoginAsync(LoginRequest request)
        {
            var validationResult = new LoginValidator().Validate(request);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, true);

            if (!result.Succeeded)
            {
                validationResult.Errors.Add(
                    new ValidationFailure(string.Empty, Localizer.GetTranslation("InvalidLoginCredentials")));

                return validationResult;
            }

            return new IdentityResponse<UserLoginResponse>(await GenerateJwtAsync(request.Email));
        }

        private async Task<UserLoginResponse> GenerateJwtAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var claims = (await _userManager.GetRolesAsync(user))
                .Select(role => new Claim("role", role))
                .ToList();

            var encodedToken = _tokenService.CodifyToken(claims, user.Id, user.Email);

            return _tokenService.GetTokenResponse(encodedToken, user.Id, user.Email, claims);
        }
    }
}
