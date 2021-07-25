using Argon.Customers.Application.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Argon.Customers.Application.Validators
{
    public class DefineMainAddressValidator : AbstractValidator<DefineMainAddressCommand>
    {
        public DefineMainAddressValidator(IStringLocalizer<DefineMainAddressValidator> localizer)
        {
            RuleFor(a => a.AddressId)
                .NotEmpty().WithMessage(localizer["Address Id is Empty"]);
        }
    }
}
