using Argon.Core.DomainObjects;
using System;

namespace Argon.Suppliers.Domain
{
    public class User : Entity
    {
        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public Gender Gender { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDelete { get; private set; }

        protected User() { }

        public User(Guid id, string firstName, string lastName, string email, Gender gender)
        {
            Check.NotEmpty(id, nameof(id));

            Id = id;
            Name = new Name(firstName, lastName);
            Email = email;
            Gender = gender;
            IsActive = true;
            IsDelete = false;
        }
    }
}
