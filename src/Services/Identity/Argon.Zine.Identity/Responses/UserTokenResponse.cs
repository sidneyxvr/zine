using System.Collections.Generic;

namespace Argon.Zine.Identity.Responses
{
    public record UserTokenResponse
    {
        public Guid Id { get; init; }
        public string Email { get; init; } = null!;
        public IEnumerable<UserClaimResponse> Claims { get; init; } = null!;
    }
}