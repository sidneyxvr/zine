using Argon.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Argon.Catalog.Domain
{
    public class Service : Entity, IAggregateRoot
    {
        public const int NameMaxLength = 100;

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public bool HasHomeAssistance { get; private set; }
        public Guid SupplierId { get; private set; }
        public Supplier? Supplier { get; private set; }
        public Guid SubCategoryId { get; private set; }
        public SubCategory? SubCategory { get; private set; }

        private List<Image> _images = new();
        public IReadOnlyCollection<Image> Images 
            => _images.AsReadOnly();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Service() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Service(string? name, string? description, decimal price, Guid supplierId, Guid subCategoryId)
        {
            Check.NotEmpty(name, nameof(name));
            Check.NotEmpty(description, nameof(description));
            Check.Range(price, 0, decimal.MaxValue, nameof(price));
            Check.NotEmpty(supplierId, nameof(supplierId));
            Check.NotEmpty(subCategoryId, nameof(subCategoryId));

            Name = name!;
            Description = description!;
            Price = price;
            SupplierId = supplierId;
            SubCategoryId = subCategoryId;
        }

        public void AddImage(string url)
        {
            _images ??= new();

            var lastImageOrder = _images?.OrderBy(i => i.Order)?.Last()?.Order ?? 0;

            _images!.Add(new Image(url, lastImageOrder));
        }
    }
}
