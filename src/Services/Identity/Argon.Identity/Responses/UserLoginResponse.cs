namespace Argon.Identity.Responses
{
    public class UserLoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenResponse UserToken { get; set; }
    }
}
