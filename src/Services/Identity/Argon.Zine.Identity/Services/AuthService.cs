using Argon.Zine.Identity.Data;
using Argon.Zine.Identity.Models;
using Argon.Zine.Identity.Requests;
using Argon.Zine.Identity.Responses;
using Argon.Zine.Identity.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Argon.Zine.Identity.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IRefreshTokenStore _refreshTokenStore;
        private readonly IStringLocalizer<AuthService> _localizer;
        private readonly IStringLocalizerFactory _localizerFactory;

        public AuthService(
            ITokenService tokenService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IRefreshTokenStore refreshTokenStore,
            IStringLocalizer<AuthService> localizer,
            IStringLocalizerFactory localizerFactory)
        {
            _localizer = localizer;
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _localizerFactory = localizerFactory;
            _refreshTokenStore = refreshTokenStore;
        }

        public async Task<IdentityResponse<UserLoginResponse>> LoginAsync(LoginRequest request)
        {
            var localizer = _localizerFactory.Create(typeof(LoginValidator));
            var validationResult = new LoginValidator(localizer).Validate(request);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return WithError(_localizer["Invalid Login Credentials"]);
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, true);

            if (result.IsNotAllowed)
            {
                return WithError(_localizer["Invalid Login Credentials"]);
            }

            if (!result.Succeeded)
            {
                return WithError(_localizer["Invalid Login Credentials"]);
            }

            var loginResponse = await GenerateUserLoginResponseAsync(user);

            if (loginResponse is null)
            {
                return WithError(_localizer["Invalid Login Credentials"]);
            }

            return new IdentityResponse<UserLoginResponse>(loginResponse);
        }

        public async Task<IdentityResponse<UserLoginResponse>> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var validationResult = new RefreshTokenValidator().Validate(request);

            if (!validationResult.IsValid)
            {
                return WithError(_localizer["Cannot Refresh Token"]);
            }

            var claimsSimplified = _tokenService.GetUserClaimsSimplifiedOrDefault(request.AccessToken!);

            if (claimsSimplified is null)
            {
                return WithError(_localizer["Cannot Refresh Token"]);
            }

            var refreshToken = await _refreshTokenStore.GetByTokenAsync(request.RefreshToken!);

            if (refreshToken is null || refreshToken.JwtId != claimsSimplified.Value.Jti ||
               refreshToken.UserId != claimsSimplified.Value.UserId || !refreshToken.IsValid)
            {
                return WithError(_localizer["Cannot Refresh Token"]);
            }

            var user = await _userManager.FindByIdAsync(claimsSimplified.Value.UserId.ToString());

            if (user is null || !user.IsActive)
            {
                return WithError(_localizer["Cannot Refresh Token"]);
            }

            refreshToken.Revoked = DateTime.UtcNow;
            var result = await _refreshTokenStore.UpdateAsync(refreshToken);

            if (!result.Succeeded)
            {
                return WithError(_localizer["Cannot Refresh Token"]);
            }

            var loginResponse = await GenerateUserLoginResponseAsync(user);

            if (loginResponse is null)
            {
                return WithError(_localizer["Cannot Refresh Token"]);
            }

            return new IdentityResponse<UserLoginResponse>(loginResponse);
        }

        private async Task<UserLoginResponse?> GenerateUserLoginResponseAsync(User user)
        {
            var claims = (await _userManager.GetRolesAsync(user))
                .Select(role => new Claim("role", role))
                .ToList();

            var encodedToken = _tokenService.CodifyToken(claims, user);

            var refreshToken = _tokenService.GenerateRefreshToken(encodedToken);

            if (refreshToken is null)
            {
                return null;
            }

            await _refreshTokenStore.CreateAsync(refreshToken);

            return _tokenService.GetUserLoginResponse(encodedToken, refreshToken.Token, user.Id, user.Email, claims);
        }
    }
}
