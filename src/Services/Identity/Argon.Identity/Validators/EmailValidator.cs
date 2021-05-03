using Argon.Core.DomainObjects;
using Argon.Core.Messages.IntegrationCommands.Validators;
using Argon.Identity.Requests;
using FluentValidation;

namespace Argon.Identity.Validators
{
    public class EmailValidator : BaseValidator<EmailRequest>
    {
        public EmailValidator()
        {
            RuleFor(a => a.Email)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyEmail"))
                .Length(Email.MinLength, Email.MaxLength)
                    .WithMessage(Localizer.GetTranslation("EmailOutOfRange", Email.MinLength, Email.MaxLength))
                .DependentRules(() =>
                {
                    RuleFor(c => c.Email).EmailAddress().WithMessage(Localizer.GetTranslation("InvalidEmail"));
                });

        }
    }
}
