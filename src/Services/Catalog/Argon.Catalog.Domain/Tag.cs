using Argon.Core.DomainObjects;
using System;
using System.Collections.Generic;

namespace Argon.Catalog.Domain
{
    public class Tag : Entity, IAggregateRoot
    {
        public const int MaxLength = 25;

        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }

        public ICollection<Service> Services 
        { 
            get => throw new InvalidOperationException(nameof(Services)); 
        }

        public Tag(string name)
        {
            Check.NotEmpty(name, nameof(name));
            Check.MaxLength(name, MaxLength, nameof(name));

            Name = name;
            IsActive = true;
            IsDeleted = false;
        }
    }
}
