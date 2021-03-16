using Argon.Core.DomainObjects;
using Argon.Suppliers.Domain;
using System.Collections.Generic;

namespace Argon.Supplier.Domain
{
    public class Supplier : Entity
    {
        public string CorporateName { get; private set; }
        public string TradeName { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDelete { get; private set; }
        public bool IsSuspended { get; private set; }
        public bool IsOpen { get; private set; }
        public CpfCnpj CpfCnpj { get; private set; }
        public Location Location { get; private set; }
        public Address Address { get; private set; }

        private List<User> _users;
        public IReadOnlyCollection<User> Users => _users.AsReadOnly();
    }
}
