using System;

namespace Argon.Identity.Models
{
    public class RefreshToken
    {
        public Guid JwtId { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public int ValidityInHours { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? Revoked { get; set; }
        public string ConcurrencyStamp { get; set; }

        public bool IsExpired => DateTime.UtcNow >= CreatedAt.AddHours(ValidityInHours);
        public bool IsValid => Revoked is null && !IsExpired;
    }
}
