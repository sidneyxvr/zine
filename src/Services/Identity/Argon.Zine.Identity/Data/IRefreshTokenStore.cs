using Argon.Zine.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Argon.Zine.Identity.Data;

public interface IRefreshTokenStore
{
    Task<IdentityResult> CreateAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
    Task<IdentityResult> UpdateAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
    Task<RefreshToken?> GetByTokenAsync(string token);
}