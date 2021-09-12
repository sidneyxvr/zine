namespace Argon.Zine.Identity.Configurations
{
    public class JwtSettings
    {
        public string Secret { get; set; } = null!;
        public string ValidOn { get; set; } = null!;
        public string Emitter { get; set; } = null!;
        public int ValidityInHours { get; set; }
        public int RefreshTokenValidityInHours { get; set; }
    }
}
