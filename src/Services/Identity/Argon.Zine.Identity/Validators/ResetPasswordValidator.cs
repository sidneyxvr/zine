using Argon.Zine.Core.DomainObjects;
using Argon.Zine.Identity.Requests;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Argon.Zine.Identity.Validators;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordValidator(IStringLocalizer localizer)
    {
        RuleFor(a => a.Email)
           .NotEmpty().WithMessage(localizer["Required Email"])
           .Length(Email.MinLength, Email.MaxLength).WithMessage(localizer["Invalid Email Length"])
           .DependentRules(() =>
           {
               RuleFor(c => c.Email).EmailAddress().WithMessage(localizer["Invalid Email"]);
           });

        RuleFor(c => c.Password)
            .NotEmpty().WithMessage(localizer["Required Password"])
            .Length(8, 100).WithMessage(localizer["Invalid Password Length"]);

        RuleFor(r => r.Token)
            .NotEmpty().WithMessage(localizer["Required Token"]);
    }
}