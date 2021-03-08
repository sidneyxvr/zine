using Argon.Core.DomainObjects;
using Argon.Core.Messages.IntegrationCommands.Validators;
using Argon.Customers.Application.Commands.CustomerCommands;
using FluentValidation;
using System;

namespace Argon.Customers.Application.Commands.Validators.CustomerValidators
{
    public class UpdateCustomerValidator : BaseValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerValidator()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyFirstName"))
                .MaximumLength(Name.MaxLengthFirstName).WithMessage(string.Format(Localizer.GetTranslation("MaxLengthFirstName"), Name.MaxLengthFirstName));

            RuleFor(c => c.Surname)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptySurname"))
                .MaximumLength(Name.MaxLengthSurname).WithMessage((string.Format(Localizer.GetTranslation("MaxLengthSurname"), Name.MaxLengthSurname)));

            RuleFor(c => c.BirthDate)
                .InclusiveBetween(DateTime.UtcNow.AddYears(-100), DateTime.UtcNow.AddYears(-18)).WithMessage(Localizer.GetTranslation("InvalidBirthDate"));

            RuleFor(c => c.Phone)
                .Must(p => Phone.IsValid(p)).WithMessage(Localizer.GetTranslation("InvalidPhone"));

            RuleFor(c => c.Gender)
                .IsInEnum().WithMessage(Localizer.GetTranslation("InvalidGender"));
        }
    }
}
