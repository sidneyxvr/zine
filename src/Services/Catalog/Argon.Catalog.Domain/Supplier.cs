using Argon.Core.DomainObjects;
using System.Collections.Generic;

namespace Argon.Catalog.Domain
{
    public class Supplier : Entity
    {
        public const int NameMaxLength = 100;
        public const int AddressMaxLength = 255;

        public string Name { get; private set; }
        public bool IsAvailable { get; private set; }
        public bool IsOpen { get; private set; }
        public Location Location { get; private set; }
        public string Address { get; private set; }

#pragma warning disable IDE0044 // Add readonly modifier
        private List<Product> _products;
#pragma warning restore IDE0044 // Add readonly modifier
        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

        protected Supplier() { }

        public Supplier(string name, double? latitude, double? longitude, string address)
        {
            Check.NotEmpty(name, nameof(name));
            Check.NotEmpty(address, nameof(address));
            Check.NotNull(latitude, nameof(latitude));
            Check.NotNull(longitude, nameof(longitude));

            Name = name;
            IsAvailable = false;
            IsOpen = false;
            Location = new Location(latitude, longitude);
            Address = address;
        }
    }
}
