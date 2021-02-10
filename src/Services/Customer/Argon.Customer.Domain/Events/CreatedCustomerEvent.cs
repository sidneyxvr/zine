using Argon.Core.DomainObjects;
using Argon.Core.Messages;
using System;

namespace Argon.Customers.Domain.Events
{
    public class CreatedCustomerEvent : Event
    {
        public Guid CustomerId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public Gender Gender { get; private set; }

        public CreatedCustomerEvent(Guid customerId, string name, string email, Gender gender)
        {
            CustomerId = customerId;
            Name = name;
            Email = email;
            Gender = gender;
        }
    }
}
