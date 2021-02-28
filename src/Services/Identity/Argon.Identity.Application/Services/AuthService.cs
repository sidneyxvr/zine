using Argon.Identity.Application.Models;
using Argon.Identity.Application.Responses;
using Argon.Identity.Application.Validations;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Argon.Identity.Application.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly SignInManager<IdentityUser<Guid>> _signInManager;

        public AuthService(SignInManager<IdentityUser<Guid>> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IdentityResponse<UserLoginResponse>> LoginAsync(LoginRequest request)
        {
            var validationResult = new LoginValidation().Validate(request);
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

            return new IdentityResponse<UserLoginResponse>(GenerateJwt(request.Email));
        }

        private UserLoginResponse GenerateJwt(string email)
        {
            return new UserLoginResponse();
        }
    }
}
