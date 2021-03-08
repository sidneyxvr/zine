using Argon.Core.Messages.IntegrationCommands.Validators;
using Argon.Identity.Requests;
using FluentValidation;

namespace Argon.Identity.Validators
{
    public class LoginValidator : BaseValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(l => l.Email)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyEmail"))
                .EmailAddress().WithMessage(Localizer.GetTranslation("InvalidEmail"));

            RuleFor(l => l.Password)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyPassword"));
        }
    }
}
