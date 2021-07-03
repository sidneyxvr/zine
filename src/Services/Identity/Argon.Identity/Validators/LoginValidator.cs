using Argon.Identity.Requests;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Argon.Identity.Validators
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator(IStringLocalizer localizer)
        {
            RuleFor(l => l.Email)
                .NotEmpty().WithMessage(localizer["Required Email"])
                .EmailAddress().WithMessage(localizer["Invalid Email"]);

            RuleFor(l => l.Password)
                .NotEmpty().WithMessage(localizer["EmptyPassword"]);
        }
    }
}
