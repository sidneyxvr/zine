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

        private readonly List<Service> _services = new();
        public IReadOnlyCollection<Service> Services 
            => _services.AsReadOnly();

        private readonly List<FeeHomeAssistance> _feeHomeAssistances = new();
        public IReadOnlyCollection<FeeHomeAssistance> FeeHomeAssistances 
            => _feeHomeAssistances.AsReadOnly();

        private readonly List<Tag> _tags = new();
        public IReadOnlyCollection<Tag> Tags 
            => _tags.AsReadOnly();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Supplier() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Supplier(string? name, double? latitude, double? longitude, string? address)
        {
            Check.NotEmpty(name, nameof(name));
            Check.NotEmpty(address, nameof(address));
            Check.NotNull(latitude, nameof(latitude));
            Check.NotNull(longitude, nameof(longitude));

            Name = name!;
            IsAvailable = false;
            IsOpen = false;
            Location = new Location(latitude, longitude);
            Address = address!;
        }
    }
}
