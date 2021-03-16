using Argon.Identity.Validators;
using FluentValidation.Results;

namespace Argon.Identity.Requests
{
    public class UpdateTwoFactorAuthenticationRequest : BaseRequest
    {
        public string Email { get; set; }
        public bool EnableTwoFactorAuthentication { get; set; }

        public override ValidationResult Validate()
            => new UpdateTwoFactorAuthenticationValidator().Validate(this);
    }
}
