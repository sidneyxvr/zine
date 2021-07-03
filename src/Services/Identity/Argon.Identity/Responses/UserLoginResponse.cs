namespace Argon.Identity.Responses
{
    public class UserLoginResponse
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public double ExpiresIn { get; set; }
        public UserTokenResponse UserToken { get; set; } = null!;
    }
}
