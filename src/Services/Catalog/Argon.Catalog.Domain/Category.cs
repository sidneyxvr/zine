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
        public Department? Department { get; private set; }

        private readonly List<SubCategory>? _subCategories = new();
        public IReadOnlyCollection<SubCategory> SubCategories 
            => _subCategories!.AsReadOnly();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Category() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Category(string? name, string? description, Guid departmentId)
        {
            Check.NotEmpty(name, nameof(name));
            Check.NotEmpty(description, nameof(description));
            Check.NotEmpty(departmentId, nameof(departmentId)); 
            
            Name = name!;
            Description = description!;
            DepartmentId = departmentId;
            IsActive = true;
        }
    }
}
