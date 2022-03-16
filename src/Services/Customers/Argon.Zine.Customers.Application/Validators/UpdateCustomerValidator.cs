using Argon.Zine.Commom.DomainObjects;
using Argon.Zine.Customers.Application.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Argon.Zine.Customers.Application.Validators;

public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerValidator(IStringLocalizer<UpdateCustomerValidator> localizer)
    {
        RuleFor(c => c.FirstName)
            .NotEmpty().WithMessage(localizer["Required First Name"])
            .MaximumLength(Name.MaxLengthFirstName).WithMessage(localizer["First Name's Max Length"]);

        RuleFor(c => c.LastName)
            .NotEmpty().WithMessage(localizer["Required Last Name"])
            .MaximumLength(Name.MaxLengthLastName).WithMessage(localizer["Last Name's Max Length"]);

        RuleFor(c => c.BirthDate)
            .InclusiveBetween(
                DateTime.UtcNow.AddYears(-BirthDate.MaxAge),
                DateTime.UtcNow.AddYears(-BirthDate.MinAge))
            .WithMessage(localizer["Invalid Birthdate"]);

        When(c => c.Phone is not null, () =>
        {
            RuleFor(c => c.Phone)
                .Must(p => Phone.IsValid(p)).WithMessage(localizer["Invalid Phone"]);
        });
    }
}