using Argon.Zine.Core.DomainObjects;
using Argon.Zine.Core.Messages.IntegrationCommands;
using Argon.Zine.Core.Utils;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;

namespace Argon.Customers.Application.Validators
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator(IStringLocalizer<CreateCustomerValidator> localizer)
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage(localizer["Required First Name"])
                .MaximumLength(Name.MaxLengthFirstName).WithMessage(localizer["First Name's Max Length"]);

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage(localizer["Required Last Name"])
                .MaximumLength(Name.MaxLengthLastName).WithMessage(localizer["Last Name's Max Length"]);

            RuleFor(c => c.Cpf)
                .NotEmpty().WithMessage(localizer["Required CPF"])
                .DependentRules(() =>
                {
                    RuleFor(c => c.Cpf).Must(c => CpfValidator.IsValid(c!)).WithMessage(localizer["Invalid CPF"]);
                });

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage(localizer["Required Email"])
                .Length(Email.MinLength, Email.MaxLength).WithMessage(localizer["Invalid Email Length"])
                .DependentRules(() =>
                {
                    RuleFor(c => c.Email).EmailAddress().WithMessage(localizer["Invalid Email"]);
                });

            RuleFor(c => c.BirthDate)
                .InclusiveBetween(DateTime.UtcNow.AddYears(-BirthDate.MaxAge), DateTime.UtcNow.AddYears(-BirthDate.MinAge))
                    .WithMessage(localizer["Invalid Birthdate"]);

            When(c => c.Phone is not null, () =>
            {
                RuleFor(c => c.Phone)
                    .Must(p => Phone.IsValid(p)).WithMessage(localizer["Invalid Phone"]);
            });

            RuleFor(c => c.Gender)
                .IsInEnum().WithMessage(localizer["Invalid Gender"]);
        }
    }
}
