namespace Argon.Identity.Configurations
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string ValidOn { get; set; }
        public string Emitter { get; set; }
        public int ValidityInHours { get; set; }
        public int RefreshTokenValidityInHours { get; set; }
    }
}
