using Argon.Core.DomainObjects;
using Argon.Core.Messages;
using System;

namespace Argon.Customers.Application.Commands
{
    public record UpdateCustomerCommand : Command
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Phone { get; init; }
        public DateTime BirthDate { get; init; }
        public Gender Gender { get; init; }
    }
}
