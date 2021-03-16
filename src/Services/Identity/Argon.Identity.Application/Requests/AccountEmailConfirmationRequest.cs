using Argon.Identity.Validators;
using FluentValidation.Results;

namespace Argon.Identity.Requests
{
    public class EmailAccountConfirmationRequest : BaseRequest
    {
        public string Email { get; set; }
        public string Token { get; set; }


        public override ValidationResult Validate()
            => new EmailAccountConfirmationValidator().Validate(this);
    }
}
