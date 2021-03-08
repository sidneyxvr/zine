using Argon.Identity.Requests;
using Argon.Identity.Validators;
using FluentValidation.Results;

namespace Argon.Identity.Requests
{
    public class AccountEmailConfirmationRequest : BaseRequest
    {
        public string Email { get; set; }
        public string Token { get; set; }


        public override ValidationResult Validate()
            => new AccountEmailConfirmationValidator().Validate(this);
    }
}
