using Argon.Core.DomainObjects;
using FluentValidation;
using System;

namespace Argon.Core.Messages.IntegrationCommands.Validators
{
    public class CreateCustomerValidator : BaseValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyFirstName"))
                .MaximumLength(Name.MaxLengthFirstName).WithMessage(Localizer.GetTranslation("MaxLengthFirstName", Name.MaxLengthFirstName));

            RuleFor(c => c.Surname)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptySurname"))
                .MaximumLength(Name.MaxLengthSurname).WithMessage(Localizer.GetTranslation("MaxLengthSurname", Name.MaxLengthSurname));

            RuleFor(c => c.Cpf)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyCPF"))
                .DependentRules(() =>
                {
                    RuleFor(c => c.Cpf).Must(c => Cpf.IsValid(c)).WithMessage(Localizer.GetTranslation("InvalidCPF"));
                });

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyEmail"))
                .Length(Email.AddressMinLength, Email.AddressMaxLength)
                    .WithMessage(Localizer.GetTranslation("EmailOutOfRange", Email.AddressMinLength, Email.AddressMaxLength))
                .DependentRules(() =>
                {
                    RuleFor(c => c.Email).EmailAddress().WithMessage(Localizer.GetTranslation("InvalidEmail"));
                });

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
