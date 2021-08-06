using Argon.Identity.Models;
using Argon.Identity.Responses;
using System.Collections.Generic;
using System.Security.Claims;

namespace Argon.Identity.Services
{
    public interface ITokenService
    {
        RefreshToken? GenerateRefreshToken(string token);
        (Guid UserId, Guid Jti)? GetUserClaimsSimplifiedOrDefault(string token);
        string CodifyToken(ICollection<Claim> claims, User user);
        UserLoginResponse GetUserLoginResponse(string encodedToken, 
            string refreshToken, Guid userId, string userEmail, IEnumerable<Claim> claims);
    }
}
