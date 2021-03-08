using Argon.Identity.Validators;
using FluentValidation.Results;

namespace Argon.Identity.Requests
{
    public class ResetPasswordRequest : BaseRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public override ValidationResult Validate()
            => new ResetPasswordValidator().Validate(this);
    }
}
