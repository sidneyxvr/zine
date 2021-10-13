using Argon.Zine.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Argon.Zine.Customers.Domain
{
    public class Customer : Entity, IAggregateRoot
    {
        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public Cpf Cpf { get; private set; }
        public BirthDate BirthDate { get; private set; }
        public Phone? Phone { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public bool IsSuspended { get; private set; }

        public Guid? MainAddressId { get; private set; }
        public Address? MainAddress { get; private set; }

        private readonly List<Address> _addresses = new();
        public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();


#pragma warning disable CS8618 
        private Customer()
#pragma warning restore CS8618 
            => _addresses = new ();

        public Customer(Guid id, string? firstName, string? LastName, string? email,
            string? cpf, DateTime? birthDate, string? phone)
        {
            Check.NotEmpty(id, nameof(id));

            Id = id;
            Name = new Name(firstName, LastName);
            Email = email;
            Cpf = cpf;
            BirthDate = birthDate!;
            Phone = phone;
            IsActive = true;
            IsDeleted = false;
            IsSuspended = true;
        }

        public void Update(string? firstName, string? LastName, DateTime birthDate)
        {
            Name = new Name(firstName, LastName);
            BirthDate = birthDate;
        }

        public void Enable() => IsActive = true;

        public void Disable() => IsActive = false;

        public void Delete() => IsDeleted = true;

        public void Suspend() => IsSuspended = true;

        public void Resume() => IsSuspended = false;

        public void AddAddress(Address address)
        {
            Check.NotNull(address, nameof(address));

            _addresses.Add(address);
        }

        public void UpdateAddress(Address address)
        {
            Check.NotNull(address, nameof(address));

            _addresses.Add(address);
        }

        public void DeleteAddress(Guid addressId)
        {
            Check.NotEmpty(addressId, nameof(addressId));

            _addresses?.RemoveAll(a => a.Id == addressId);

            if (MainAddress?.Id == addressId)
            {
                MainAddress = null;
            }
        }

        public void DefineMainAddress(Guid addressId)
        {
            var address = _addresses?.FirstOrDefault(a => a.Id == addressId);

            Check.NotNull(address, nameof(address));

            MainAddress = address;
        }
    }
}
