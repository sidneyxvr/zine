using Argon.Core.DomainObjects;
using Argon.Core.Messages.IntegrationCommands.Validators;
using Argon.Identity.Requests;
using FluentValidation;

namespace Argon.Identity.Validators
{
    public class EmailAccountConfirmationValidator : BaseValidator<EmailAccountConfirmationRequest>
    {
        public EmailAccountConfirmationValidator()
        {
            RuleFor(a => a.Email)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyEmail"))
                .Length(Email.MinLength, Email.MaxLength)
                    .WithMessage(Localizer.GetTranslation("EmailOutOfRange"))
                .DependentRules(() =>
                {
                    RuleFor(c => c.Email).EmailAddress().WithMessage(Localizer.GetTranslation("InvalidEmail"));
                });

            RuleFor(c => c.Token)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyToken"));
        }
    }
}
