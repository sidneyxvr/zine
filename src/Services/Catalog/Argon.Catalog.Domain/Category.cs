using Argon.Core.DomainObjects;
using System;
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
        public bool IsActive { get; set; }
        public Guid DepartmentId { get; private set; }
        public Department Department { get; private set; }


        private List<SubCategory> _subCategories;
        public IReadOnlyCollection<SubCategory> SubCategories => _subCategories.AsReadOnly();

        protected Category() { }

        public Category(string name, string description, Guid departmentId)
        {
            Name = name;
            Description = description;
            DepartmentId = departmentId;
            IsActive = true;
        }
    }
}
