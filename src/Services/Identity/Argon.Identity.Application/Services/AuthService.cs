using Argon.Identity.Data;
using Argon.Identity.Models;
using Argon.Identity.Requests;
using Argon.Identity.Responses;
using Argon.Identity.Validators;
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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IRefreshTokenStore _refreshTokenStore;

        public AuthService(
            ITokenService tokenService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IRefreshTokenStore refreshTokenStore)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _refreshTokenStore = refreshTokenStore;
        }

        public async Task<IdentityResponse<UserLoginResponse>> LoginAsync(LoginRequest request)
        {
            var validationResult = new LoginValidator().Validate(request);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return NotifyError(Localizer.GetTranslation("InvalidLoginCredentials"));
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, true);

            if (result.IsNotAllowed)
            {
                return NotifyError(Localizer.GetTranslation("InvalidLoginCredentials"));
            }

            if (!result.Succeeded)
            {
                return NotifyError(Localizer.GetTranslation("InvalidLoginCredentials"));
            }

            return new IdentityResponse<UserLoginResponse>(await GenerateUserLoginResponseAsync(user));
        }

        public async Task<IdentityResponse<UserLoginResponse>> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var validationResult = request.Validate();
            if (!validationResult.IsValid)
            {
                return NotifyError(Localizer.GetTranslation("CannotRefreshToken"));
            }

            var claimsSimplified = _tokenService.GetUserClaimsSimplifiedOrDefault(request.AccessToken);

            if(claimsSimplified is null)
            {
                return NotifyError(Localizer.GetTranslation("CannotRefreshToken"));
            }

            var refreshToken = await _refreshTokenStore.GetByTokenAsync(request.RefreshToken);

            if(refreshToken.JwtId != claimsSimplified.Value.Jti || 
               refreshToken.UserId != claimsSimplified.Value.UserId ||
               !refreshToken.IsValid)
            {
                return NotifyError(Localizer.GetTranslation("CannotRefreshToken"));
            }

            var user = await _userManager.FindByIdAsync(claimsSimplified.Value.UserId.ToString());

            if (user is null || !user.IsActive)
            {
                return NotifyError(Localizer.GetTranslation("CannotRefreshToken"));
            }

            refreshToken.Revoked = DateTime.UtcNow;
            var result = await _refreshTokenStore.UpdateAsync(refreshToken);

            if (!result.Succeeded)
            {
                return NotifyError(Localizer.GetTranslation("CannotRefreshToken"));
            }

            return new IdentityResponse<UserLoginResponse>(await GenerateUserLoginResponseAsync(user));
        }

        private async Task<UserLoginResponse> GenerateUserLoginResponseAsync(User user)
        {
            var claims = (await _userManager.GetRolesAsync(user))
                .Select(role => new Claim("role", role))
                .ToList();

            var encodedToken = _tokenService.CodifyToken(claims, user.Id, user.Email);

            var refreshToken = _tokenService.GenerateRefreshToken(encodedToken);

            await _refreshTokenStore.CreateAsync(refreshToken);

            return _tokenService.GetUserLoginResponse(encodedToken, refreshToken.Token, user.Id, user.Email, claims);
        }
    }
}
