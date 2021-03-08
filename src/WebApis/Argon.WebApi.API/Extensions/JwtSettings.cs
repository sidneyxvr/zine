namespace Argon.WebApi.API.Extensions
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string ValidOn { get; set; }
        public string Emitter { get; set; }
        public int HourValidation { get; set; }
    }
}
