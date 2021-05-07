using Argon.Core.DomainObjects;
using System.Collections.Generic;

namespace Argon.Suppliers.Domain
{
    public class Supplier : Entity
    {
        public string CorporateName { get; private set; }
        public string TradeName { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDelete { get; private set; }
        public bool IsSuspended { get; private set; }
        public CpfCnpj CpfCnpj { get; private set; }
        public Address Address { get; private set; }

        private List<User> _users;
        public IReadOnlyCollection<User> Users => _users.AsReadOnly();

        protected Supplier() { }

        public Supplier(string corparateName, string tradeName, 
            string cpfCnpj, User user, Address address)
        {
            CorporateName = corparateName;
            TradeName = tradeName;
            CpfCnpj = cpfCnpj;
            Address = address;

            IsActive = true;
            IsDelete = false;
            IsSuspended = false;

            AddUser(user);
        }

        public void AddUser(User user)
        {
            _users ??= new();
            _users.Add(user);
        }
    }
}
