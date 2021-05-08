using Argon.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Argon.Customers.Domain
{
    public class Customer : Entity, IAggregateRoot
    {
        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public Cpf Cpf { get; private set; }
        public BirthDate BirthDate { get; private set; }
        public Phone Phone { get; private set; }
        public Gender Gender { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public bool IsSuspended { get; private set; }

        public Guid? MainAddressId { get; set; }
        public Address MainAddress { get; private set; }

        private readonly List<Address> _addresses;
        public IReadOnlyCollection<Address> Addresses => _addresses?.AsReadOnly();


        protected Customer() 
        {
            _addresses = new ();
        }

        public Customer(Guid id, string firstName, string LastName, string email,
            string cpf, DateTime? birthDate, Gender gender, string phone)
        {
            Id = id;
            Name = new Name(firstName, LastName);
            Email = email;
            Cpf = cpf;
            Gender = gender;
            BirthDate = birthDate;
            Phone = phone;
            IsActive = true;
            IsDeleted = false;
            IsSuspended = true;
            _addresses = new();

            Validate();
        }

        public void Update(string firstName, string LastName, DateTime birthDate, Gender gender)
        {
            Name = new Name(firstName, LastName);
            Gender = gender;
            BirthDate = birthDate;

            Validate();
        }

        public void Enable() => IsActive = true;

        public void Disable() => IsActive = false;

        public void Delete() => IsDeleted = true;

        public void Suspend() => IsSuspended = true;

        public void Resume() => IsSuspended = false;

        public void AddAddress(Address address)
        {
            _addresses.Add(address ?? throw new ArgumentNullException(nameof(address)));
        }

        public void DeleteAddress(Guid addressId)
        {
            var address = _addresses?.FirstOrDefault(a => a.Id == addressId);

            Check.NotNull(address, nameof(address));

            if(MainAddress.Id == address.Id)
            {
                MainAddress = null;
            }

            _addresses.Remove(address);
        }

        public void DefineMainAddress(Guid addressId)
        {
            var address = _addresses?.FirstOrDefault(a => a.Id == addressId);

            Check.NotNull(address, nameof(address));

            MainAddress = address;
        }

        private void Validate()
        {
            Check.IsEnum(Gender, typeof(Gender), nameof(Gender));
        }
    }
}
