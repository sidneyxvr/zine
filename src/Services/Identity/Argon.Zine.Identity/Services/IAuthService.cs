using Argon.Zine.Identity.Requests;
using Argon.Zine.Identity.Responses;

namespace Argon.Zine.Identity.Services;

public interface IAuthService
{
    Task<IdentityResult<UserLoginResponse>> LoginAsync(LoginRequest request);
    Task<IdentityResult<UserLoginResponse>> RefreshTokenAsync(RefreshTokenRequest request);
}