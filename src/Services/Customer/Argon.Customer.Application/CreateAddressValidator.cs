﻿using Argon.Core.Messages.IntegrationCommands.Validators;
using Argon.Customers.Domain;
using FluentValidation;

namespace Argon.Customers.Application
{
    public class CreateAddressValidator : BaseValidator<CreateAddressCommand>
    {
        public CreateAddressValidator()
        {
            RuleFor(a => a.City)
                .NotNull().WithMessage(Localizer.GetTranslation("EmptyCity"))
                .Length(2, 40).WithMessage(Localizer.GetTranslation("CityOutOfRange", Address.CityMinLength, Address.CityMaxLength));

            RuleFor(a => a.District)
                .NotNull().WithMessage(Localizer.GetTranslation("EmptyDistrict"))
                .Length(2, 50).WithMessage(Localizer.GetTranslation("DistrictOutOfRange", Address.DistrictMinLength, Address.DistrictMaxLength));

            RuleFor(a => a.Street)
                .NotNull().WithMessage(Localizer.GetTranslation("EmptyStreet"))
                .Length(2, 50).WithMessage(Localizer.GetTranslation("StreetOutOfRange", Address.StreetMinLength, Address.StreetMaxLength));

            RuleFor(a => a.State)
                .NotNull().WithMessage(Localizer.GetTranslation("EmptyState"))
                .Length(2).WithMessage(Localizer.GetTranslation("InvalidState"));

            RuleFor(a => a.PostalCode)
                .NotNull().WithMessage(Localizer.GetTranslation("EmptyPostalCode"))
                .Length(8).WithMessage(Localizer.GetTranslation("InvalidPostalCode"));

            RuleFor(a => a.Number)
                .MaximumLength(10).WithMessage(Localizer.GetTranslation("NumberMaxLength", Address.NumberMaxLength));

            RuleFor(a => a.Complement)
                .MaximumLength(50).WithMessage(Localizer.GetTranslation("ComplementMaxLength", Address.ComplementMaxLength));

            RuleFor(a => a.CustomerId)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyCustomerId"));

            RuleFor(a => a.Latitude)
                .GreaterThanOrEqualTo(-90).WithMessage(Localizer.GetTranslation("InvalidLatitude"))
                .LessThanOrEqualTo(90).WithMessage(Localizer.GetTranslation("InvalidLatitude"));

            RuleFor(a => a.Longitude)
                .GreaterThanOrEqualTo(-180).WithMessage(Localizer.GetTranslation("InvalidLongitude"))
                .LessThanOrEqualTo(180).WithMessage(Localizer.GetTranslation("InvalidLongitude"));

            When(a => a.Latitude is null && a.Longitude is not null, () =>
            {
                RuleFor(l => l.Longitude)
                    .Null().WithMessage(Localizer.GetTranslation("InvalidCoordinates"));
            });

            When(a => a.Latitude is not null && a.Longitude is null, () =>
            {
                RuleFor(l => l.Longitude)
                   .Null().WithMessage(Localizer.GetTranslation("InvalidCoordinates"));
            });
        }
    }
}