using Argon.Zine.Identity.Models;
using Argon.Zine.Identity.Responses;
using System.Security.Claims;

namespace Argon.Zine.Identity.Services;

public interface ITokenService
{
    RefreshToken? GenerateRefreshToken(string token);
    (Guid UserId, Guid Jti)? GetUserClaimsSimplifiedOrDefault(string token);
    string CodifyToken(ICollection<Claim> claims, User user);
    UserLoginResponse GetUserLoginResponse(string encodedToken,
        string refreshToken, Guid userId, string userEmail, IEnumerable<Claim> claims);
}