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
        public Category Category { get; private set; }

        private List<Service> _products;
        public IReadOnlyCollection<Service> Products => _products.AsReadOnly();

        protected SubCategory() { }

        public SubCategory(string name, string description, Guid categoryId)
        {
            Name = name;
            Description = description;
            CategoryId = categoryId;
            IsActive = true;
        }
    }
}
