using Argon.Zine.Identity.Requests;
using Argon.Zine.Identity.Responses;
using System.Threading.Tasks;

namespace Argon.Zine.Identity.Services
{
    public interface IAuthService
    {
        Task<IdentityResponse<UserLoginResponse>> LoginAsync(LoginRequest request);
        Task<IdentityResponse<UserLoginResponse>> RefreshTokenAsync(RefreshTokenRequest request);
    }
}
