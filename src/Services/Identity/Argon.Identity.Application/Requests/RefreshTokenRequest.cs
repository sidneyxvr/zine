using Argon.Identity.Validators;
using FluentValidation.Results;

namespace Argon.Identity.Requests
{
    public class RefreshTokenRequest : BaseRequest
    {
        public string AccessToken { get; init; }
        public string RefreshToken { get; init; }
        public override ValidationResult Validate()
            => new RefreshTokenValidator().Validate(this);
    }
}
