using Argon.Core.DomainObjects;
using Argon.Core.Messages.Events;
using System;

namespace Argon.Customers.Domain.Events
{
    public class CreatedCustomerEvent : Event
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Gender Gender { get; private set; }

        public CreatedCustomerEvent(Guid aggregateId, string name, string email, string phone, DateTime birthDate, Gender gender)
        {
            AggregateId = aggregateId;
            Name = name;
            Email = email;
            Phone = phone;
            BirthDate = birthDate;
            Gender = gender;
        }
    }
}
