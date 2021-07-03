using Argon.Core.DomainObjects;
using System;
using System.Collections.Generic;

namespace Argon.Catalog.Domain
{
    public class SubCategory : Entity
    {
        public const int NameMinLength = 3;
        public const int NameMaxLength = 25;
        public const int DescriptionMaxLength = 255;

        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; set; }
        public Guid CategoryId { get; private set; }
        public Category? Category { get; private set; }

        private readonly List<Service> _products = new ();
        public IReadOnlyCollection<Service> Products 
            => _products.AsReadOnly();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected SubCategory() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public SubCategory(string? name, string? description, Guid categoryId)
        {
            Check.NotEmpty(name, nameof(name)); 
            Check.NotEmpty(description, nameof(description));       

            Name = name!;
            Description = description!;
            CategoryId = categoryId;
            IsActive = true;
        }
    }
}
