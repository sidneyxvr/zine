using Argon.Identity.Validators;
using FluentValidation.Results;

namespace Argon.Identity.Requests
{
    public class EmailRequest : BaseRequest
    {
        public string Email { get; set; }

        public override ValidationResult Validate()
            => new EmailValidator().Validate(this);
    }
}
