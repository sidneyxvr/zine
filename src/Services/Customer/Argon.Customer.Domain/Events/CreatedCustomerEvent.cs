using Argon.Core.DomainObjects;
using Argon.Core.Messages;
using System;

namespace Argon.Customers.Domain.Events
{
    public class CreatedCustomerEvent : Event
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public Gender Gender { get; private set; }

        public CreatedCustomerEvent(Guid aggregateId, string name, string email, Gender gender)
        {
            AggregateId = aggregateId;
            Name = name;
            Email = email;
            Gender = gender;
        }
    }
}
