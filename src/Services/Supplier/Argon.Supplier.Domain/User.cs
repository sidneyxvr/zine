using Argon.Core.DomainObjects;
using System;

namespace Argon.Suppliers.Domain
{
    public class User : Entity<Guid>
    {
        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public Gender Gender { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDelete { get; private set; }

        public Guid SupplierId { get; private set; }
        public Supplier? Supplier { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected User() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public User(Guid id, string? firstName, string? lastName, string? email, Gender gender)
            : base(NewGuid())
        {
            Check.NotEmpty(id, nameof(id));
            Check.NotEmpty(firstName, nameof(firstName));   
            Check.NotEmpty(lastName, nameof(lastName));
            Check.IsEnum(gender, typeof(Gender), nameof(Gender));

            Id = id;
            Name = new Name(firstName!, lastName!);
            Email = email;
            Gender = gender;
            IsActive = true;
            IsDelete = false;
        }
    }
}
