using Argon.Core.DomainObjects;
using Argon.Core.Messages.IntegrationCommands.Validators;
using Argon.Customers.Domain;
using FluentValidation;

namespace Argon.Customers.Application
{
    public class UpdateAddressValidator : BaseValidator<UpdateAddressCommand>
    {
        public UpdateAddressValidator()
        {
            RuleFor(a => a.City)
                .NotNull().WithMessage(Localizer.GetTranslation("EmptyCity"))
                .Length(Address.CityMinLength, Address.CityMaxLength).WithMessage(Localizer.GetTranslation("CityOutOfRange", Address.CityMinLength, Address.CityMaxLength));

            RuleFor(a => a.District)
                .NotNull().WithMessage(Localizer.GetTranslation("EmptyDistrict"))
                .Length(Address.DistrictMinLength, Address.DistrictMaxLength).WithMessage(Localizer.GetTranslation("DistrictOutOfRange", Address.DistrictMinLength, Address.DistrictMaxLength));

            RuleFor(a => a.Street)
                .NotNull().WithMessage(Localizer.GetTranslation("EmptyStreet"))
                .Length(Address.StreetMinLength, Address.StreetMaxLength).WithMessage(Localizer.GetTranslation("StreetOutOfRange", Address.StreetMinLength, Address.StreetMaxLength));

            RuleFor(a => a.State)
                .NotNull().WithMessage(Localizer.GetTranslation("EmptyState"))
                .Length(Address.StateLength).WithMessage(Localizer.GetTranslation("InvalidState", Address.StateLength));

            RuleFor(a => a.PostalCode)
                .NotNull().WithMessage(Localizer.GetTranslation("EmptyPostalCode"))
                .Length(Address.PostalCodeLength).WithMessage(Localizer.GetTranslation("InvalidPostalCode", Address.PostalCodeLength));

            RuleFor(a => a.Number)
                .MaximumLength(Address.NumberMaxLength).WithMessage(Localizer.GetTranslation("NumberMaxLength", Address.NumberMaxLength));

            RuleFor(a => a.Complement)
                .MaximumLength(Address.ComplementMaxLength).WithMessage(Localizer.GetTranslation("ComplementMaxLength", Address.ComplementMaxLength));

            RuleFor(a => a.AddressId)
               .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyAddressId"));

            RuleFor(a => a.CustomerId)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyCustomerId"));

            RuleFor(a => a.Latitude)
                .GreaterThanOrEqualTo(Location.MinLatitude).WithMessage(Localizer.GetTranslation("InvalidLatitude"))
                .LessThanOrEqualTo(Location.MaxLatitude).WithMessage(Localizer.GetTranslation("InvalidLatitude"));

            RuleFor(a => a.Longitude)
                .GreaterThanOrEqualTo(Location.MinLongitude).WithMessage(Localizer.GetTranslation("InvalidLongitude"))
                .LessThanOrEqualTo(Location.MaxLongitude).WithMessage(Localizer.GetTranslation("InvalidLongitude"));

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
