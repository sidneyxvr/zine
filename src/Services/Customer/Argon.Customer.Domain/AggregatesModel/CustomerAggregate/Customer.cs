using Argon.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Argon.Customers.Domain.AggregatesModel.CustomerAggregate
{
    public class Customer : Entity, IAggregaeteRoot
    {
        public FullName FullName { get; private set; }
        public Email Email { get; private set; }
        public Cpf Cpf { get; private set; }
        public DateTime BirthDate { get; set; }
        public Phone Phone { get; set; }
        public Gender Gender { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDelete { get; private set; }
        public bool IsSuspended { get; private set; }

        public Address MainAddress { get; private set; }

        private List<Address> _addresses;
        public IReadOnlyCollection<Address> Addresses => _addresses?.AsReadOnly();

        public const int MinAge = 18;
        public const int MaxAge = 100;

        protected Customer() { }

        public Customer(Guid id, string fullName, string email, 
            string cpf, DateTime birthDate, Gender gender, string phone)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            Cpf = cpf;

            ValidateGender(gender);
            Gender = gender;

            ValidateBirthDate(birthDate);
            BirthDate = birthDate;

            Phone = phone;

            IsActive = true;
            IsDelete = false;
            IsSuspended = false;
        }

        public void Update(string fullName, DateTime birthDate, Gender gender)
        {
            FullName = fullName;

            ValidateGender(gender);
            Gender = gender;

            ValidateBirthDate(birthDate);
            BirthDate = birthDate;
        }

        public void Enable() => IsActive = true;

        public void Disable() => IsActive = false;

        public void Delete() => IsDelete = true;

        public void Suspend() => IsSuspended = true;

        public void Resume() => IsSuspended = false;

        public void AddAddress(string street, string number, string district, 
            string city, string state, string country, string postalCode, string complement)
        {
            _addresses ??= new List<Address>();

            _addresses.Add(new Address(street, number, district, city, state, country, postalCode, complement));
        }

        public void DeleteAddress(Guid addressId)
        {
            var address = _addresses?.FirstOrDefault(a => a.Id == addressId);

            AssertionConcern.AssertArgumentNotNull(address, Localizer.GetTranslation("AddressNotFound"));

            _addresses.Remove(address);
        }

        public void SetAddressAsMain(Guid addressId)
        {
            var address = _addresses?.FirstOrDefault(a => a.Id == addressId);

            AssertionConcern.AssertArgumentNotNull(address, Localizer.GetTranslation("AddressNotFound"));

            MainAddress = address;
        }

        private void ValidateBirthDate(DateTime birthDate)
        {
            AssertionConcern.AssertArgumentMin(birthDate, DateTime.UtcNow.AddYears(-MinAge), Localizer.GetTranslation("MinBirthDate"));

            AssertionConcern.AssertArgumentMax(birthDate, DateTime.UtcNow.AddYears(-MaxAge), Localizer.GetTranslation("MaxBirthDate"));
        }
        
        private void ValidateGender(Gender gender)
        {
            AssertionConcern.AssertIsEnum(gender, typeof(Gender), Localizer.GetTranslation("InvalidGender"));
        }
    }
}
