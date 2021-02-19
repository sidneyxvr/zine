using Argon.Core.DomainObjects;
using Argon.Core.Messages.IntegrationCommands.Validations;
using FluentValidation;
using System;

namespace Argon.Identity.Application.Commands.Validations
{
    public class CreateUserValidation : BaseValidation<CreateUserCommand>
    {
        public CreateUserValidation()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyFirstName"))
                .MaximumLength(Name.MaxLengthFirstName).WithMessage(Localizer.GetTranslation("MaxLengthFirstName"));

            RuleFor(c => c.Surname)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptySurname"))
                .MaximumLength(Name.MaxLengthSurname).WithMessage(Localizer.GetTranslation("MaxLengthSurname"));

            RuleFor(c => c.Cpf)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyCPF"))
                .Must(c => Cpf.IsValid(c)).WithMessage(Localizer.GetTranslation("InvalidCPF"));

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("EmptyEmail")
                .Length(Email.AddressMinLength, Email.AddressMaxLength).WithMessage(Localizer.GetTranslation("EmailOutOfRange"))
                .Must(e => Email.IsValid(e)).WithMessage(Localizer.GetTranslation("InvalidEmail"));

            RuleFor(c => c.BirthDate)
                .InclusiveBetween(DateTime.UtcNow.AddYears(-100), DateTime.UtcNow.AddYears(-18)).WithMessage(Localizer.GetTranslation("InvalidBirthDate"));

            RuleFor(c => c.Phone)
                .Must(p => Phone.IsValid(p)).WithMessage(Localizer.GetTranslation("InvalidPhone"));

            RuleFor(c => c.Gender)
                .NotEmpty().WithMessage("EmptyGender")
                .IsInEnum().WithMessage("InvalidGender");
        }
    }
}
