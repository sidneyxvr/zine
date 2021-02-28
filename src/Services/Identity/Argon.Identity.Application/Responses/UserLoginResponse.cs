using FluentValidation.Results;

namespace Argon.Identity.Application.Responses
{
    public class UserLoginResponse
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenResponse UserToken { get; set; }
    }
}
