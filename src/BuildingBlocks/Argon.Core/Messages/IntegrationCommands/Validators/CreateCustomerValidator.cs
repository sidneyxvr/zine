using Argon.Core.DomainObjects;
using Argon.Core.Utils;
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

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyLastName"))
                .MaximumLength(Name.MaxLengthLastName).WithMessage(Localizer.GetTranslation("MaxLengthLastName", Name.MaxLengthLastName));

            RuleFor(c => c.Cpf)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyCPF"))
                .DependentRules(() =>
                {
                    RuleFor(c => c.Cpf).Must(c => CpfValidator.IsValid(c)).WithMessage(Localizer.GetTranslation("InvalidCPF"));
                });

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyEmail"))
                .Length(Email.MinLength, Email.MaxLength).WithMessage(Localizer.GetTranslation("EmailOutOfRange", Email.MinLength, Email.MaxLength))
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
