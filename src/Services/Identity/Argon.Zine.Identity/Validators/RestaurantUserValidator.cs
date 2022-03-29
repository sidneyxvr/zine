using Argon.Zine.Commom.DomainObjects;
using Argon.Zine.Commom.Utils;
using Argon.Zine.Identity.Requests;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Argon.Zine.Identity.Validators;

public class RestaurantUserValidator : AbstractValidator<RestaurantUserRequest>
{
    public RestaurantUserValidator(IStringLocalizer localizer)
    {
        RuleFor(c => c.TradeName)
            .NotEmpty().WithMessage(localizer["EmptyTradeName"]);
             //.Length(MinLengthTradeName, MaxLengthTradeName)
             //   .WithMessage(localizer["TradeNameOutOfRange"]));

        When(c => !c.IsCompany, () =>
        {
            RuleFor(c => c.CpfCnpj)
                .NotEmpty().WithMessage(localizer["Empty CPF"])
                .Must(cpfCnpj => Cpf.IsValid(cpfCnpj!)).WithMessage(localizer["InvalidCPF"]);
        });

        When(c => c.IsCompany, () =>
        {
            RuleFor(c => c.CorparateName)
                .NotEmpty().WithMessage(localizer["Empty Corparate Name"]);
                //.Length(MinLengthCorporateName, MaxLengthCorporateName).WithMessage(Localizer.GetTranslation("CorparateNameOutOfRange"));

            RuleFor(c => c.CpfCnpj)
                .NotEmpty().WithMessage(localizer["Empty CNPJ"])
                .Must(cpfCnpj => Cnpj.IsValid(cpfCnpj!)).WithMessage(localizer["Invalid CNPJ"]);
        });

        //User
        RuleFor(c => c.FirstName)
            .NotEmpty().WithMessage(localizer["Empty First Name"])
            .MaximumLength(Name.MaxLengthFirstName).WithMessage(localizer["MaxLengthFirstName"]);

        RuleFor(c => c.Surname)
            .NotEmpty().WithMessage(localizer["EmptySurname"])
            .MaximumLength(Name.MaxLengthSurname).WithMessage(localizer["MaxLengthSurname"]);

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage(localizer["Empty Email"])
            .Length(Email.MinLength, Email.MaxLength)
                .WithMessage(localizer["Email Out Of Range"])
            .DependentRules(() =>
            {
                RuleFor(c => c.Email).EmailAddress().WithMessage(localizer["Invalid Email"]);
            });

        RuleFor(c => c.Password)
            .NotEmpty().WithMessage(localizer["Empty Password"])
            .Length(8, 100).WithMessage(localizer["Password Out Of Range"]);

        //Address
        RuleFor(a => a.City)
            .NotNull().WithMessage(localizer["Empty City"])
            .Length(2, 40).WithMessage(localizer["City Out Of Range"]);

        RuleFor(a => a.District)
            .NotNull().WithMessage(localizer["EmptyDistrict"])
            .Length(2, 50).WithMessage(localizer["DistrictOutOfRange"]);

        RuleFor(a => a.Street)
            .NotNull().WithMessage(localizer["EmptyStreet"])
            .Length(2, 50).WithMessage(localizer["StreetOutOfRange"]);

        RuleFor(a => a.State)
            .NotNull().WithMessage(localizer["EmptyState"])
            .Length(2).WithMessage(localizer["InvalidState"]);

        RuleFor(a => a.PostalCode)
            .NotNull().WithMessage(localizer["EmptyPostalCode"])
            .Length(8).WithMessage(localizer["InvalidPostalCode"]);

        RuleFor(a => a.Number)
            .MaximumLength(10).WithMessage(localizer["NumberMaxLength"]);

        RuleFor(a => a.Complement)
            .MaximumLength(50).WithMessage(localizer["ComplementMaxLength"]);

        RuleFor(a => a.Latitude)
            .GreaterThanOrEqualTo(-90).WithMessage(localizer["InvalidLatitude"])
            .LessThanOrEqualTo(90).WithMessage(localizer["InvalidLatitude"]);

        RuleFor(a => a.Longitude)
            .GreaterThanOrEqualTo(-180).WithMessage(localizer["InvalidLongitude"])
            .LessThanOrEqualTo(180).WithMessage(localizer["InvalidLongitude"]);

        When(a => a.Latitude is null && a.Longitude is not null, () =>
        {
            RuleFor(l => l.Longitude)
                .Null().WithMessage(localizer["InvalidCoordinates"]);
        });

        When(a => a.Latitude is not null && a.Longitude is null, () =>
        {
            RuleFor(l => l.Longitude)
               .Null().WithMessage(localizer["InvalidCoordinates"]);
        });
    }
}