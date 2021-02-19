using Argon.Core.DomainObjects;
using FluentValidation;
using System;

namespace Argon.Core.Messages.IntegrationCommands.Validations
{
    public class CreateCustomerValidation : BaseValidation<CreateCustomerCommand>
    {
        public CreateCustomerValidation()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyFirstName"))
                .MaximumLength(Name.MaxLengthFirstName).WithMessage(string.Format(Localizer.GetTranslation("MaxLengthFirstName"), Name.MaxLengthFirstName));

            RuleFor(c => c.Surname)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptySurname"))
                .MaximumLength(Name.MaxLengthSurname).WithMessage(string.Format(Localizer.GetTranslation("MaxLengthSurname"), Name.MaxLengthSurname));

            RuleFor(c => c.Cpf)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyCPF"))
                .Must(c => Cpf.IsValid(c)).WithMessage(Localizer.GetTranslation("InvalidCPF"));

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyEmail"))
                .EmailAddress().WithMessage(Localizer.GetTranslation("InvalidEmail"))
                .Length(Email.AddressMinLength, Email.AddressMaxLength).WithMessage(string.Format(Localizer.GetTranslation("EmailOutOfRange"), Email.AddressMinLength, Email.AddressMaxLength));

            RuleFor(c => c.BirthDate)
                .InclusiveBetween(DateTime.UtcNow.AddYears(-BirthDate.MaxAge), DateTime.UtcNow.AddYears(-BirthDate.MinAge)).WithMessage(Localizer.GetTranslation("InvalidBirthDate"));

            RuleFor(c => c.Phone)
                .Must(p => Phone.IsValid(p)).WithMessage(Localizer.GetTranslation("InvalidPhone"));

            RuleFor(c => c.Gender)
                .IsInEnum().WithMessage(Localizer.GetTranslation("InvalidGender"));
        }
    }
}
