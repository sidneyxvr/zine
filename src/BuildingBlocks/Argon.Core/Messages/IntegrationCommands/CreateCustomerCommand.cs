using Argon.Core.DomainObjects;
using Argon.Core.Messages.IntegrationCommands.Validators;
using System;

namespace Argon.Core.Messages.IntegrationCommands
{
    public class CreateCustomerCommand : Command
    {
        public Guid UserId { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
        public string Cpf { get; init; }
        public DateTime BirthDate { get; init; }
        public Gender Gender { get; init; }
        public string ConfirmationToken { get; init; }

        public override bool IsValid()
        {
            ValidationResult = new CreateCustomerValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
