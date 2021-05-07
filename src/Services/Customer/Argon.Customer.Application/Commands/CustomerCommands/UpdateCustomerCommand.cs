using Argon.Core.DomainObjects;
using Argon.Core.Messages;
using Argon.Customers.Application.Commands.Validators.CustomerValidators;
using System;

namespace Argon.Customers.Application.Commands.CustomerCommands
{
    public class UpdateCustomerCommand : Command
    {
        public Guid CustomerId { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Phone { get; init; }
        public DateTime BirthDate { get; init; }
        public Gender Gender { get; init; }

        public override bool IsValid()
        {
            ValidationResult = new UpdateCustomerValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
