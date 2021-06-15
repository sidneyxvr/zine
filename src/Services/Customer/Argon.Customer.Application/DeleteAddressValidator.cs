using Argon.Core.Messages.IntegrationCommands.Validators;
using FluentValidation;

namespace Argon.Customers.Application
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
