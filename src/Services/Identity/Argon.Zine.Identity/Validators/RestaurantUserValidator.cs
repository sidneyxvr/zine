using Argon.Zine.Identity.Requests;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Argon.Zine.Identity.Validators
{
    public class RestaurantUserValidator : AbstractValidator<RestaurantUserRequest>
    {
        public RestaurantUserValidator(IStringLocalizer localizer)
        {
            //RuleFor(c => c.TradeName)
            //    .NotEmpty().WithMessage(localizer["EmptyTradeName"])
            //    .Length(MinLengthTradeName, MaxLengthTradeName)
            //        .WithMessage(Localizer.GetTranslation("TradeNameOutOfRange"));

            //When(c => !c.IsCompany, () =>
            //{
            //    RuleFor(c => c.CpfCnpj)
            //        .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyCPF"))
            //        .Must(cpfCnpj => CpfValidator.IsValid(cpfCnpj!)).WithMessage(Localizer.GetTranslation("InvalidCPF"));
            //});

            //When(c => c.IsCompany, () =>
            //{
            //    RuleFor(c => c.CorparateName)
            //        .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyCorparateName"))
            //        .Length(MinLengthCorporateName, MaxLengthCorporateName).WithMessage(Localizer.GetTranslation("CorparateNameOutOfRange"));

            //    RuleFor(c => c.CpfCnpj)
            //        .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyCNPJ"))
            //        .Must(cpfCnpj => CnpjValidator.IsValid(cpfCnpj!)).WithMessage(Localizer.GetTranslation("InvalidCNPJ"));
            //});

            ////User
            //RuleFor(c => c.FirstName)
            //    .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyFirstName"))
            //    .MaximumLength(Name.MaxLengthFirstName).WithMessage(Localizer.GetTranslation("MaxLengthFirstName"));

            //RuleFor(c => c.LastName)
            //    .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyLastName"))
            //    .MaximumLength(Name.MaxLengthLastName).WithMessage(Localizer.GetTranslation("MaxLengthLastName"));

            //RuleFor(c => c.Email)
            //    .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyEmail"))
            //    .Length(Email.MinLength, Email.MaxLength)
            //        .WithMessage(Localizer.GetTranslation("EmailOutOfRange"))
            //    .DependentRules(() =>
            //    {
            //        RuleFor(c => c.Email).EmailAddress().WithMessage(Localizer.GetTranslation("InvalidEmail"));
            //    });

            //RuleFor(c => c.Gender)
            //    .IsInEnum().WithMessage(Localizer.GetTranslation("InvalidGender"));

            //RuleFor(c => c.Password)
            //    .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyPassword"))
            //    .Length(8, 100).WithMessage(Localizer.GetTranslation("PasswordOutOfRange"));

            ////Address
            //RuleFor(a => a.City)
            //    .NotNull().WithMessage(Localizer.GetTranslation("EmptyCity"))
            //    .Length(2, 40).WithMessage(Localizer.GetTranslation("CityOutOfRange"));

            //RuleFor(a => a.District)
            //    .NotNull().WithMessage(Localizer.GetTranslation("EmptyDistrict"))
            //    .Length(2, 50).WithMessage(Localizer.GetTranslation("DistrictOutOfRange"));

            //RuleFor(a => a.Street)
            //    .NotNull().WithMessage(Localizer.GetTranslation("EmptyStreet"))
            //    .Length(2, 50).WithMessage(Localizer.GetTranslation("StreetOutOfRange"));

            //RuleFor(a => a.State)
            //    .NotNull().WithMessage(Localizer.GetTranslation("EmptyState"))
            //    .Length(2).WithMessage(Localizer.GetTranslation("InvalidState"));

            //RuleFor(a => a.PostalCode)
            //    .NotNull().WithMessage(Localizer.GetTranslation("EmptyPostalCode"))
            //    .Length(8).WithMessage(Localizer.GetTranslation("InvalidPostalCode"));

            //RuleFor(a => a.Number)
            //    .MaximumLength(10).WithMessage(Localizer.GetTranslation("NumberMaxLength"));

            //RuleFor(a => a.Complement)
            //    .MaximumLength(50).WithMessage(Localizer.GetTranslation("ComplementMaxLength"));

            //RuleFor(a => a.Latitude)
            //    .GreaterThanOrEqualTo(-90).WithMessage(Localizer.GetTranslation("InvalidLatitude"))
            //    .LessThanOrEqualTo(90).WithMessage(Localizer.GetTranslation("InvalidLatitude"));

            //RuleFor(a => a.Longitude)
            //    .GreaterThanOrEqualTo(-180).WithMessage(Localizer.GetTranslation("InvalidLongitude"))
            //    .LessThanOrEqualTo(180).WithMessage(Localizer.GetTranslation("InvalidLongitude"));

            //When(a => a.Latitude is null && a.Longitude is not null, () =>
            //{
            //    RuleFor(l => l.Longitude)
            //        .Null().WithMessage(Localizer.GetTranslation("InvalidCoordinates"));
            //});

            //When(a => a.Latitude is not null && a.Longitude is null, () =>
            //{
            //    RuleFor(l => l.Longitude)
            //       .Null().WithMessage(Localizer.GetTranslation("InvalidCoordinates"));
            //});
        }
    }
}
