using Argon.Identity.Responses;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Argon.Identity.Services
{
    public interface ITokenService
    {
        string CodifyToken(ICollection<Claim> claims, Guid userId, string userEmail);
        UserLoginResponse GetTokenResponse(string encodedToken, Guid userId, string userEmail, IEnumerable<Claim> claims);
    }
}
