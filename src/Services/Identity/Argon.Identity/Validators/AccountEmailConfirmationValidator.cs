using Argon.Core.DomainObjects;
using Argon.Identity.Requests;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Argon.Identity.Validators
{
    public class EmailAccountConfirmationValidator : AbstractValidator<EmailAccountConfirmationRequest>
    {
        public EmailAccountConfirmationValidator(IStringLocalizer localizer)
        {
            RuleFor(a => a.Email)
                .NotEmpty().WithMessage(localizer["Required Email"])
                .Length(Email.MinLength, Email.MaxLength).WithMessage(localizer["Invalid Email Length"])
                .DependentRules(() =>
                {
                    RuleFor(c => c.Email).EmailAddress().WithMessage(localizer["Invalid Email"]);
                });

            RuleFor(c => c.Token)
                .NotEmpty().WithMessage(localizer["Required Token"]);
        }
    }
}
