using Argon.Core.DomainObjects;
using System;

namespace Argon.Catalog.Domain
{
    public class Product : Entity
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public Guid SupplierId { get; private set; }
        public Supplier? Supplier { get; private set; }

#pragma warning disable CS8618
        protected Product() { }
#pragma warning restore CS8618

        public Product(string name, decimal price, Guid supplierId)
        {
            Name = name;
            Price = price;
            SupplierId = supplierId;
        }
    }
}
