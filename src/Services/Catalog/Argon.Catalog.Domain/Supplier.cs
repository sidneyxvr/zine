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
        public bool HasHomeAssistance { get; private set; }

        private List<Service> _services;
        public IReadOnlyCollection<Service> Services => _services.AsReadOnly();

        private List<FeeHomeAssistance> _feeHomeAssistances;
        public IReadOnlyCollection<FeeHomeAssistance> FeeHomeAssistances => _feeHomeAssistances.AsReadOnly();

        private List<Tag> _tags;
        public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

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
