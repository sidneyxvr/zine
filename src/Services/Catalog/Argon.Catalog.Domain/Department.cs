using Argon.Core.DomainObjects;
using System.Collections.Generic;

namespace Argon.Catalog.Domain
{
    public class Department : Entity, IAggregateRoot
    {
        public const int NameMinLength = 3;
        public const int NameMaxLength = 25;
        public const int DescriptionMaxLength = 255;

        public string Name { get; private set; }
        public string? Description { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }

        private readonly List<Category> _categories = new();
        public IReadOnlyCollection<Category> Categories 
            => _categories.AsReadOnly();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Department() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Department(string? name, string? description)
        {
            Check.NotEmpty(name, nameof(name));
            Check.Length(name!, NameMinLength, NameMaxLength, nameof(description));   
            Check.MaxLength(description, DescriptionMaxLength, nameof(description));   

            Name = name!;
            Description = description;
            IsActive = true;
        }
    }
}
