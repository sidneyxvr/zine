using Argon.Core.DomainObjects;

namespace Argon.Catalog.Domain
{
    public class Product : Entity
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public Supplier Supplier { get; private set; }
    }
}
