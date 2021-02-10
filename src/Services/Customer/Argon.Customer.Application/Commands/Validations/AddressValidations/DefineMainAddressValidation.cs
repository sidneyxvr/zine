using Argon.Core.Messages.IntegrationCommands.Validations;
using Argon.Customers.Application.Commands.AddressCommands;
using FluentValidation;

namespace Argon.Customers.Application.Commands.Validations.AddressValidations
{
    public class DefineMainAddressValidation : BaseValidation<DefineMainAddressCommand>
    {
        public DefineMainAddressValidation()
        {
            RuleFor(a => a.CustomerId)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyCustomerId"));

            RuleFor(a => a.AddressId)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyAddressId"));
        }
    }
}
