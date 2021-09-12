using Argon.Zine.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Zine.Identity.Data
{
    public class RefreshTokenStore : IRefreshTokenStore
    {
        private readonly IdentityContext _context;
        public RefreshTokenStore(IdentityContext context)
        {
            _context = context;
        }

        public async Task<IdentityResult> CreateAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (refreshToken == null)
            {
                throw new ArgumentNullException(nameof(refreshToken));
            }

            refreshToken.ConcurrencyStamp = Guid.NewGuid().ToString();
            _context.Add(refreshToken);
            await _context.SaveChangesAsync(cancellationToken);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (refreshToken == null)
            {
                throw new ArgumentNullException(nameof(refreshToken));
            }

            _context.Attach(refreshToken);
            refreshToken.ConcurrencyStamp = NewGuid().ToString();
            _context.Update(refreshToken);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "ConcurrencyFailure",
                    Description = "Optimistic concurrency failure, object has been modified."
                });
            }
            return IdentityResult.Success;
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token);
        }
    }
}
