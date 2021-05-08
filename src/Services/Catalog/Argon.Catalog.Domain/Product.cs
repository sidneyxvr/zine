using Argon.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Argon.Catalog.Domain
{
    public class Product : Entity
    {
        public const int NameMaxLength = 100;

        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public Guid SupplierId { get; private set; }
        public Supplier Supplier { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; }

        private List<Image> _images;
        public IReadOnlyCollection<Image> Images => _images.AsReadOnly();

        protected Product() { }

        public Product(string name, decimal price, Guid supplierId, Guid categoryId)
        {
            Name = name;
            Price = price;
            SupplierId = supplierId;
            CategoryId = categoryId;
        }

        public void AddImage(string url)
        {
            _images ??= new();

            var lastImageOrder = _images?.OrderBy(i => i.Order)?.Last()?.Order ?? 0;

            _images.Add(new Image(url, lastImageOrder));
        }
    }
}
