using Argon.Core.DomainObjects;
using System;

namespace Argon.Customers.Domain.AggregatesModel.CustomerAggregate
{
    public class Address : Entity
    {
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string District { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string PostalCode { get; private set; }
        public string Complement { get; private set; }
        public Guid CustomerId { get; private set; }
        protected Address() { }
    }
}
