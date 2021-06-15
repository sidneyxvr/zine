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
        public Supplier Supplier { get; private set; }
        public Guid SubCategoryId { get; private set; }
        public SubCategory SubCategory { get; private set; }

        private List<Image> _images;
        public IReadOnlyCollection<Image> Images => _images.AsReadOnly();

        protected Service() { }

        public Service(string name, decimal price, Guid supplierId, Guid subCategoryId)
        {
            Name = name;
            Price = price;
            SupplierId = supplierId;
            SubCategoryId = subCategoryId;
        }

        public void AddImage(string url)
        {
            _images ??= new();

            var lastImageOrder = _images?.OrderBy(i => i.Order)?.Last()?.Order ?? 0;

            _images.Add(new Image(url, lastImageOrder));
        }
    }
}
