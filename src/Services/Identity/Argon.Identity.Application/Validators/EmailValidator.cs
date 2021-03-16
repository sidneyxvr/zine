﻿using Argon.Core.DomainObjects;
using Argon.Core.Messages.IntegrationCommands.Validators;
using Argon.Identity.Requests;
using FluentValidation;

namespace Argon.Identity.Validators
{
    public class EmailValidator : BaseValidator<EmailRequest>
    {
        public EmailValidator()
        {
            RuleFor(a => a.Email)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyEmail"))
                .Length(Email.AddressMinLength, Email.AddressMaxLength)
                    .WithMessage(Localizer.GetTranslation("EmailOutOfRange", Email.AddressMinLength, Email.AddressMaxLength))
                .DependentRules(() =>
                {
                    RuleFor(c => c.Email).EmailAddress().WithMessage(Localizer.GetTranslation("InvalidEmail"));
                });

        }
    }
}
