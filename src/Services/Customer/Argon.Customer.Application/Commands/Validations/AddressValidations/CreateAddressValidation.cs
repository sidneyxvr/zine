using Argon.Core.Messages.IntegrationCommands.Validations;
using Argon.Customers.Application.Commands.AddressCommands;
using FluentValidation;

namespace Argon.Customers.Application.Commands.Validations.AddressValidations
{
    public class CreateAddressValidation : BaseValidation<CreateAddressCommand>
    {
        public CreateAddressValidation()
        {
            RuleFor(a => a.City)
                .NotEmpty().WithMessage("EmptyCity")
                .Length(2, 40).WithMessage("CityOutOfRange");

            RuleFor(a => a.Country)
                .NotEmpty().WithMessage("EmptyCountry")
                .Length(2, 50).WithMessage("CountryOutOfRange");

            RuleFor(a => a.District)
                .NotEmpty().WithMessage("EmptyDisctrict")
                .Length(2, 50).WithMessage("DistrictOutOfRange");

            RuleFor(a => a.Street)
                .NotEmpty().WithMessage("EmptyStreet")
                .Length(2, 50).WithMessage("StreetOutOfRange");

            RuleFor(a => a.State)
                .NotEmpty().WithMessage("EmptyState")
                .Length(2).WithMessage("InvalidState");

            RuleFor(a => a.PostalCode)
                .NotEmpty().WithMessage("EmptyPostalCode")
                .Length(8).WithMessage("InvalidPostalCodde");

            RuleFor(a => a.Number)
                .MaximumLength(5).WithMessage("NumberMaxLength");

            RuleFor(a => a.Complement)
                .MaximumLength(50).WithMessage("ComplementMaxLength");

            RuleFor(a => a.CustomerId)
                .NotEmpty().WithMessage("EmptyCustomerId");
        }
    }
}