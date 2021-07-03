using Argon.Core.DomainObjects;
using System;
using System.Collections.Generic;

namespace Argon.Suppliers.Domain
{
    public class Supplier : Entity
    {
        public const int CorporateNameMaxLength = 50;
        public const int CorporateNameMinLength = 2;
        public const int TradeNameMaxLength = 50;
        public const int TradeNameMinLength = 2;

        public string CorporateName { get; private set; }
        public string TradeName { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public bool IsSuspended { get; private set; }
        public CpfCnpj CpfCnpj { get; private set; }

        public Guid AddressId { get; set; }
        public Address Address { get; private set; }

        private List<User> _users = new();
        public IReadOnlyCollection<User> Users => _users.AsReadOnly();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Supplier() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Supplier(string? corparateName, string? tradeName, 
            string? cpfCnpj, User? user, Address? address)
        {
            Check.NotEmpty(corparateName, nameof(corparateName));
            Check.NotEmpty(tradeName, nameof(tradeName));
            Check.NotEmpty(cpfCnpj, nameof(cpfCnpj));
            Check.NotNull(address, nameof(address));
            Check.NotNull(user, nameof(user));

            CorporateName = corparateName!;
            TradeName = tradeName!;
            CpfCnpj = cpfCnpj!;
            Address = address!;

            IsActive = true;
            IsDeleted = false;
            IsSuspended = false;

            AddUser(user!);
        }

        public void AddUser(User user)
        {
            _users ??= new();
            _users.Add(user);
        }
    }
}
