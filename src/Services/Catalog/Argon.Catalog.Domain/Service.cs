global using static System.Guid;
using Argon.Core.DomainObjects;
using System;
using System.Collections.Generic;

namespace Argon.Catalog.Domain
{
    public class Service : Entity<Guid>, IAggregateRoot
    {
        public const int NameMaxLength = 100;
        public const int DescriptionMaxLength = 255;

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public bool HasHomeAssistance { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public Guid SupplierId { get; private set; }
        public Supplier? Supplier { get; private set; }
        public Guid SubCategoryId { get; private set; }
        public SubCategory? SubCategory { get; private set; }

        private List<Image> _images = new();
        public IReadOnlyCollection<Image> Images 
            => _images.AsReadOnly();

        private List<FeeHomeAssistance> _feeHomeAssistances = new();
        public IReadOnlyCollection<FeeHomeAssistance> FeeHomeAssistances
            => _feeHomeAssistances.AsReadOnly();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Service() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Service(string? name, string? description, decimal price, Guid supplierId, Guid subCategoryId, 
            bool hasHomeAssistance, List<Image>? images, List<FeeHomeAssistance>? feeHomeAssistances)
            : base(NewGuid())
        {
            Check.NotEmpty(name, nameof(name));
            Check.NotEmpty(description, nameof(description));
            Check.Range(price, decimal.Zero, decimal.MaxValue, nameof(price));
            Check.NotEmpty(supplierId, nameof(supplierId));
            Check.NotEmpty(subCategoryId, nameof(subCategoryId));
            ValidateHomeAssistence(hasHomeAssistance, feeHomeAssistances);

            Name = name!;
            Description = description!;
            Price = price;
            SupplierId = supplierId;
            SubCategoryId = subCategoryId;
            HasHomeAssistance = hasHomeAssistance;

            _feeHomeAssistances = feeHomeAssistances ?? _feeHomeAssistances;
            _images = images ?? _images;
        }

        private static void ValidateHomeAssistence(
            bool HasHomeAssistance, List<FeeHomeAssistance>? FeeHomeAssistances)
        {
            if(HasHomeAssistance && FeeHomeAssistances?.Count > 0)
            {
                throw new InvalidOperationException(nameof(HasHomeAssistance));
            }
        }
    }
}
