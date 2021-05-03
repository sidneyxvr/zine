using Argon.Core.DomainObjects;

namespace Argon.Catalog.Domain
{
    public class Supplier : Entity
    {
        public string Name { get; private set; }
        public bool IsAvailable { get; private set; }
        public bool IsOpen { get; private set; }
        public Location Location { get; private set; }
        public string Address { get; private set; }

        public Supplier(string name, bool isAvaliable, bool isOpen, double latitude, double longitude, string address)
        {
            Name = name;
            IsAvailable = isAvaliable;
            IsOpen = isOpen;
            Location = new Location(latitude, longitude);
            Address = address;
        }
    }
}
