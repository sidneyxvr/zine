using Argon.Core.DomainObjects;
using Argon.Core.Messages.IntegrationCommands.Validations;
using Argon.Customers.Application.Commands.CustomerCommands;
using FluentValidation;
using System;

namespace Argon.Customers.Application.Commands.Validations.CustomerValidations
{
    public class UpdateCustomerValidation : BaseValidation<UpdateCustomerCommand>
    {
        public UpdateCustomerValidation()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyFirstName"))
                .MaximumLength(Name.FirstNameMaxLength).WithMessage("MaxLengthFirstName");

            RuleFor(c => c.Surname)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptySurname"))
                .MaximumLength(Name.SurnameMaxLength).WithMessage("MaxLengthSurname");

            RuleFor(c => c.BirthDate)
                .NotEmpty().WithMessage("EmptyBirthDate")
                .InclusiveBetween(DateTime.UtcNow.AddYears(-100), DateTime.UtcNow.AddYears(-18)).WithMessage("InvalidBirthDate");

            RuleFor(c => c.Phone)
                .Must(p => Phone.IsValid(p)).WithMessage("InvalidPhone");

            RuleFor(c => c.Gender)
                .NotEmpty().WithMessage("EmptyGender")
                .IsInEnum().WithMessage("InvalidGender");
        }
    }
}
