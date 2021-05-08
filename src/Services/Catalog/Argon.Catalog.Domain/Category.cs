using Argon.Core.DomainObjects;
using System.Collections.Generic;

namespace Argon.Catalog.Domain
{
    public class Category : Entity
    {
        public const int NameMinLength = 3;
        public const int NameMaxLength = 25;
        public const int DescriptionMaxLength = 255;

        public string Name { get; private set; }
        public string Description { get; private set; }

#pragma warning disable IDE0044 // Add readonly modifier
        private List<Product> _products;
#pragma warning restore IDE0044 // Add readonly modifier
        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

        protected Category() { }

        public Category(string name)
        {
            Name = name;
        }

        public Category(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
