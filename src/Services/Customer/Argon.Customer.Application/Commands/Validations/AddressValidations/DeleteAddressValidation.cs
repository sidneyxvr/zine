using Argon.Core.Messages.IntegrationCommands.Validations;
using Argon.Customers.Application.Commands.AddressCommands;
using FluentValidation;

namespace Argon.Customers.Application.Commands.Validations.AddressValidations
{
    public class DeleteAddressValidation : BaseValidation<DeleteAddressCommand>
    {
        public DeleteAddressValidation()
        {
            RuleFor(a => a.AggregateId)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyAggregateId"));

            RuleFor(a => a.AddressId)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyAddressId"));
        }
    }
}
