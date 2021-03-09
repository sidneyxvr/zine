using Argon.Identity.Managers;
using Argon.Identity.Models;
using Argon.Identity.Requests;
using Argon.Identity.Responses;
using Argon.Identity.Validators;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Argon.Identity.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(
            ITokenService tokenService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
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

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return NotifyError(Localizer.GetTranslation("InvalidLoginCredentials"));
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, true);

            if (result.IsNotAllowed)
            {
                //TODO: return email not confirmed
            }

            if (!result.Succeeded)
            {
                return NotifyError(Localizer.GetTranslation("InvalidLoginCredentials"));
            }

            return new IdentityResponse<UserLoginResponse>(await GenerateJwtAsync(user));
        }

        private async Task<UserLoginResponse> GenerateJwtAsync(ApplicationUser user)
        {
            var claims = (await _userManager.GetRolesAsync(user))
                .Select(role => new Claim("role", role))
                .ToList();

            var encodedToken = _tokenService.CodifyToken(claims, user.Id, user.Email);

            return _tokenService.GetTokenResponse(encodedToken, user.Id, user.Email, claims);
        }
    }
}
