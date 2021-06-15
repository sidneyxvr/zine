using Argon.Core.DomainObjects;
using Argon.Core.Utils;
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
        private string _cpf;
        public string Cpf { get => _cpf?.OnlyNumbers(); init => _cpf = value; }
        public DateTime BirthDate { get; init; }
        public Gender Gender { get; init; }
        public string ConfirmationToken { get; init; }
    }
}
