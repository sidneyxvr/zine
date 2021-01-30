using Argon.Core.Messages.IntegrationCommands.Validations;
using Argon.Customers.Application.Commands.AddressCommands;
using FluentValidation;

namespace Argon.Customers.Application.Commands.Validations.AddressValidations
{
    public class UpdateAddressValidation : BaseValidation<UpdateAddressCommand>
    {
        public UpdateAddressValidation()
        {
            RuleFor(a => a.City)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyCity"))
                .Length(2, 40).WithMessage(Localizer.GetTranslation("CityOutOfRange"));

            RuleFor(a => a.Country)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyCountry"))
                .Length(2, 50).WithMessage(Localizer.GetTranslation("CountryOutOfRange"));

            RuleFor(a => a.District)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyDisctrict"))
                .Length(2, 50).WithMessage(Localizer.GetTranslation("DistrictOutOfRange"));

            RuleFor(a => a.Street)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyStreet"))
                .Length(2, 50).WithMessage(Localizer.GetTranslation("StreetOutOfRange"));

            RuleFor(a => a.State)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyState"))
                .Length(2).WithMessage(Localizer.GetTranslation("InvalidState"));

            RuleFor(a => a.PostalCode)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyPostalCode"))
                .Length(8).WithMessage(Localizer.GetTranslation("InvalidPostalCodde"));

            RuleFor(a => a.Number)
                .MaximumLength(5).WithMessage(Localizer.GetTranslation("NumberMaxLength"));

            RuleFor(a => a.Complement)
                .MaximumLength(50).WithMessage(Localizer.GetTranslation("ComplementMaxLength"));

            RuleFor(a => a.AddressId)
               .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyAddressId"));

            RuleFor(a => a.AggregateId)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyAggregateId"));
        }
    }
}
