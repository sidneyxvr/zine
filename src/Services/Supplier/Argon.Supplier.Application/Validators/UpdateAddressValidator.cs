using Argon.Core.DomainObjects;
using Argon.Restaurants.Application.Commands;
using Argon.Restaurants.Domain;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Argon.Restaurants.Application.Validators
{
    public class UpdateAddressValidator : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAddressValidator(IStringLocalizer<UpdateAddressValidator> localizer)
        {
            RuleFor(a => a.City)
                .NotNull().WithMessage(localizer["Required City"])
                .Length(Address.CityMinLength, Address.CityMaxLength).WithMessage(localizer["Invalid City Length"]);

            RuleFor(a => a.District)
                .NotNull().WithMessage(localizer["Required District"])
                .Length(Address.DistrictMinLength, Address.DistrictMaxLength).WithMessage(localizer["Invalid District Length"]);

            RuleFor(a => a.Street)
                .NotNull().WithMessage(localizer["Required Street"])
                .Length(Address.StreetMinLength, Address.StreetMaxLength).WithMessage(localizer["Invalid Street Length"]);

            RuleFor(a => a.State)
                .NotNull().WithMessage(localizer["Required State"])
                .Length(Address.StateLength).WithMessage(localizer["Invalid State"]);

            RuleFor(a => a.PostalCode)
                .NotNull().WithMessage(localizer["Required Postal Code"])
                .Length(Address.PostalCodeLength).WithMessage(localizer["Invalid Postal Code"]);

            RuleFor(a => a.Number)
                .MaximumLength(Address.NumberMaxLength).WithMessage(localizer["Number's Max Length"]);

            RuleFor(a => a.Complement)
                .MaximumLength(Address.ComplementMaxLength).WithMessage(localizer["Complement's Max Length"]);

            RuleFor(a => a.AddressId)
               .NotEmpty().WithMessage(localizer["Required Address Id"]);

            RuleFor(a => a.Latitude)
                .GreaterThanOrEqualTo(Location.MinLatitude).WithMessage(localizer["Invalid Latitude"])
                .LessThanOrEqualTo(Location.MaxLatitude).WithMessage(localizer["Invalid Latitude"]);

            RuleFor(a => a.Longitude)
                .GreaterThanOrEqualTo(Location.MinLongitude).WithMessage(localizer["Invalid Longitude"])
                .LessThanOrEqualTo(Location.MaxLongitude).WithMessage(localizer["Invalid Longitude"]);

            When(a => a.Latitude is null && a.Longitude is not null, () =>
            {
                RuleFor(l => l.Longitude)
                    .Null().WithMessage(localizer["Invalid Coordinates"]);
            });

            When(a => a.Latitude is not null && a.Longitude is null, () =>
            {
                RuleFor(l => l.Longitude)
                   .Null().WithMessage(localizer["Invalid Coordinates"]);
            });
        }
    }
}
