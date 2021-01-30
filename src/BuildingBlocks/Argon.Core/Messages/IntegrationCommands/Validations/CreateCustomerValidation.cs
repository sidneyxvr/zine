﻿using Argon.Core.DomainObjects;
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
                .MaximumLength(Name.FirstNameMaxLength).WithMessage("MaxLengthFirstName");

            RuleFor(c => c.Surname)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptySurname"))
                .MaximumLength(Name.SurnameMaxLength).WithMessage("MaxLengthSurname");

            RuleFor(c => c.Cpf)
                .NotEmpty().WithMessage("EmptyCPF")
                .Must(c => Cpf.IsValid(c)).WithMessage("InvalidCPF");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("EmptyEmail")
                .Length(Email.AddressMinLength, Email.AddressMaxLength).WithMessage("EmailOutOfRange")
                .Must(e => Email.IsValid(e)).WithMessage("InvalidEmail");

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