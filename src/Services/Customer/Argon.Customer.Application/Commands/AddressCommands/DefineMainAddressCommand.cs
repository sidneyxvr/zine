using Argon.Core.Messages;
using Argon.Customers.Application.Commands.Validations.AddressValidations;
using System;

namespace Argon.Customers.Application.Commands.AddressCommands
{
    public class DefineMainAddressCommand : Command
    {
        public Guid AddressId { get; private set; }

        public DefineMainAddressCommand(Guid aggregateId, Guid addressId)
        {
            AggregateId = aggregateId;
            AddressId = addressId;
        }

        public override bool IsValid()
        {
            ValidationResult = new DefineMainAddressValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
