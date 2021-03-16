using Argon.Identity.Requests;
using Argon.Identity.Responses;
using System.Threading.Tasks;

namespace Argon.Identity.Services
{
    public interface IAuthService
    {
        Task<IdentityResponse<UserLoginResponse>> LoginAsync(LoginRequest request);
        Task<IdentityResponse<UserLoginResponse>> RefreshTokenAsync(RefreshTokenRequest request);
    }
}
