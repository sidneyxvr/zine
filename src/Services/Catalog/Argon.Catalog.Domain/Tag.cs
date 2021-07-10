using Argon.Core.DomainObjects;
using System.Collections.Generic;

namespace Argon.Catalog.Domain
{
    public class Tag : Entity, IAggregateRoot
    {
        public const int MinLength = 3;
        public const int MaxLength = 25;

        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }

        public List<Service>? Services { get; }

        public Tag(string name)
        {
            Check.NotEmpty(name, nameof(name));
            Check.Length(name, MinLength, MaxLength, nameof(name));

            Name = name;
            IsActive = true;
            IsDeleted = false;
        }
    }
}
