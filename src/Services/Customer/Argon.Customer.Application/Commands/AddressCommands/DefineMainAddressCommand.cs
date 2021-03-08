using Argon.Core.Messages;
using Argon.Customers.Application.Commands.Validators.AddressValidators;
using System;

namespace Argon.Customers.Application.Commands.AddressCommands
{
    public class DefineMainAddressCommand : Command
    {
        public Guid CustomerId { get; private set; }
        public Guid AddressId { get; private set; }

        public DefineMainAddressCommand(Guid customerId, Guid addressId)
        {
            CustomerId = customerId;
            AddressId = addressId;
        }

        public override bool IsValid()
        {
            ValidationResult = new DefineMainAddressValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
