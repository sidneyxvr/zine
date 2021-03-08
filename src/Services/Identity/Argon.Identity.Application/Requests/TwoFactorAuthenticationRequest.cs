using Argon.Identity.Validators;
using FluentValidation.Results;

namespace Argon.Identity.Requests
{
    public class TwoFactorAuthenticationRequest : BaseRequest
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public override ValidationResult Validate()
            => new TwoFactorAuthenticationValidator().Validate(this);
    }
}
