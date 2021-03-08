using Argon.Core.Messages.IntegrationCommands.Validators;
using Argon.Customers.Application.Commands.AddressCommands;
using FluentValidation;

namespace Argon.Customers.Application.Commands.Validators.AddressValidators
{
    public class DeleteAddressValidator : BaseValidator<DeleteAddressCommand>
    {
        public DeleteAddressValidator()
        {
            RuleFor(a => a.CustomerId)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyCustomerId"));

            RuleFor(a => a.AddressId)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyAddressId"));
        }
    }
}
