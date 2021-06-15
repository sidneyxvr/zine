using Argon.Core.DomainObjects;
using Argon.Core.Messages;
using System;

namespace Argon.Customers.Application
{
    public class UpdateCustomerCommand : Command
    {
        public Guid CustomerId { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Phone { get; init; }
        public DateTime BirthDate { get; init; }
        public Gender Gender { get; init; }
    }
}
