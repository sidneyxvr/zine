using Argon.Identity.Application.Models;
using Argon.Identity.Application.Responses;
using System.Threading.Tasks;

namespace Argon.Identity.Application.Services
{
    public interface IAuthService
    {
        Task<IdentityResponse<UserLoginResponse>> LoginAsync(LoginRequest request);
    }
}
