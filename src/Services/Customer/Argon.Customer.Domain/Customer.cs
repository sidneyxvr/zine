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
        public bool IsDelete { get; private set; }
        public bool IsSuspended { get; private set; }

        public Address MainAddress { get; private set; }

        private List<Address> _addresses;
        public IReadOnlyCollection<Address> Addresses => _addresses?.AsReadOnly();


        protected Customer() { }

        public Customer(Guid id, string firstName, string surname, string email,
            string cpf, DateTime? birthDate, Gender gender, string phone)
        {
            Id = id;
            Name = new Name(firstName, surname);
            Email = email;
            Cpf = cpf;
            Gender = gender;
            BirthDate = birthDate;
            Phone = phone;
            IsActive = true;
            IsDelete = false;
            IsSuspended = true;

            Validate();
        }

        public void Update(string firstName, string surname, DateTime birthDate, Gender gender)
        {
            Name = new Name(firstName, surname);
            Gender = gender;
            BirthDate = birthDate;

            Validate();
        }

        public void Enable() => IsActive = true;

        public void Disable() => IsActive = false;

        public void Delete() => IsDelete = true;

        public void Suspend() => IsSuspended = true;

        public void Resume() => IsSuspended = false;

        public void AddAddress(Address address)
        {
            _addresses ??= new List<Address>();

            _addresses.Add(address ?? throw new ArgumentNullException(nameof(address)));
        }

        public void DeleteAddress(Guid addressId)
        {
            var address = _addresses?.FirstOrDefault(a => a.Id == addressId);

            AssertionConcern.AssertArgumentFound(address, Localizer.GetTranslation("AddressNotFound"));

            if(MainAddress.Id == address.Id)
            {
                MainAddress = null;
            }

            _addresses.Remove(address);
        }

        public void UpdateAddress(Guid addressId, string street, string number, string district, string city, 
            string state, string country, string postalCode, string complement, double? latitude, double? longitude)
        {
            var address = _addresses.FirstOrDefault(a => a.Id == addressId);

            AssertionConcern.AssertArgumentFound(address, Localizer.GetTranslation("AddressNotFound"));

            address.Update(street, number, district, city, state, country, postalCode, complement, latitude, longitude);
        }

        public void DefineMainAddress(Guid addressId)
        {
            var address = _addresses?.FirstOrDefault(a => a.Id == addressId);

            AssertionConcern.AssertArgumentFound(address, Localizer.GetTranslation("AddressNotFound"));

            MainAddress = address;
        }

        private void Validate()
        {
            AssertionConcern.AssertIsEnum(Gender, typeof(Gender), Localizer.GetTranslation("InvalidGender"));
        }
    }
}
