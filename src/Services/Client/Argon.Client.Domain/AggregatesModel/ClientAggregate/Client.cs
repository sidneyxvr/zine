using Argon.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Argon.Clients.Domain.AggregatesModel.ClientAggregate
{
    public class Client : Entity, IAggregaeteRoot
    {
        public string FullName { get; private set; }
        public Email Email { get; private set; }
        public Cpf Cpf { get; private set; }
        public Gender Gender { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDelete { get; private set; }
        public bool IsSuspended { get; private set; }

        private List<Address> _addresses;
        public IReadOnlyCollection<Address> Addresses => _addresses?.AsReadOnly();

        protected Client() { }

        public Client(Guid id, string fullName, string email, string cpf, Gender gender)
        {
            Id = id;
            FullName = IsValidName(fullName) ? fullName : throw new DomainException(Localizer.GetTranslation("InvalidFullName"));
            Email = email;
            Cpf = cpf;
            Gender = gender;
            IsActive = true;
            IsDelete = false;
            IsSuspended = false;
        }

        public void AddAddress()
        {
            _addresses ??= new List<Address>();
        }

        private static bool IsValidName(string fullName) =>
            Regex.IsMatch(fullName, @"^(?![ ])(?!.*[ ]{2})((?:e|da|do|das|dos|de|d'|D'|la|las|el|los)\s*?|(?:[A-Z][^\s]*\s*?)(?!.*[ ]$))+$");
    }
}
