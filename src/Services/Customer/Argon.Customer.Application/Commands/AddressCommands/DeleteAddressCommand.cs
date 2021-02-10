using Argon.Core.Messages;
using Argon.Customers.Application.Commands.Validations.AddressValidations;
using System;

namespace Argon.Customers.Application.Commands.AddressCommands
{
    public class DeleteAddressCommand : Command
    {
        public Guid CustomerId { get; private set; }
        public Guid AddressId { get; set; }

        public DeleteAddressCommand(Guid customerId, Guid addressId)
        {
            CustomerId = customerId;
            AddressId = addressId;
        }

        public override bool IsValid()
        {
            ValidationResult = new DeleteAddressValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
