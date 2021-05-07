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
                .MaximumLength(Name.MaxLengthFirstName).WithMessage(Localizer.GetTranslation("MaxLengthFirstName"));

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyLastName"))
                .MaximumLength(Name.MaxLengthLastName).WithMessage((Localizer.GetTranslation("MaxLengthLastName")));

            RuleFor(c => c.BirthDate)
                .InclusiveBetween(DateTime.UtcNow.AddYears(-BirthDate.MaxAge), DateTime.UtcNow.AddYears(-BirthDate.MinAge))
                    .WithMessage(Localizer.GetTranslation("InvalidBirthDate"));

            When(c => c.Phone is not null, () =>
            {
                RuleFor(c => c.Phone)
                    .Must(p => Phone.IsValid(p)).WithMessage(Localizer.GetTranslation("InvalidPhone"));
            });

            RuleFor(c => c.Gender)
                .IsInEnum().WithMessage(Localizer.GetTranslation("InvalidGender"));
        }
    }
}
