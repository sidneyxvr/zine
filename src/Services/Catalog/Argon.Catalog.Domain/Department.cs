using Argon.Core.DomainObjects;
using System.Collections.Generic;

namespace Argon.Catalog.Domain
{
    public class Department : Entity
    {
        public const int NameMinLength = 3;
        public const int NameMaxLength = 25;
        public const int DescriptionMaxLength = 255;

        public string Name { get; private set; }
        public string Description { get; private set; }

        public bool IsActive { get; set; }

        private List<Category> _categories;
        public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();

        protected Department() { }

        public Department(string name, string description)
        {
            Name = name;
            Description = description;
            IsActive = true;
        }
    }
}
