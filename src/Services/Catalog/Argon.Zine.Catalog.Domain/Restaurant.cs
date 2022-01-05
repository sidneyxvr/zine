using Argon.Zine.Commom.DomainObjects;

namespace Argon.Zine.Catalog.Domain;

public class Restaurant : Entity, IAggregateRoot
{
    public const int NameMaxLength = 100;
    public const int AddressMaxLength = 255;

    public string Name { get; private set; }
    public bool IsAvailable { get; private set; }
    public bool IsOpen { get; private set; }
    public bool IsDeleted { get; private set; }
    public Location Location { get; private set; }
    public string Address { get; private set; }
    public string? LogoUrl { get; private set; }

    public List<Product>? Services { get; }

#pragma warning disable CS8618
    protected Restaurant() { }
#pragma warning restore CS8618

    public Restaurant(Guid id, string name,
        double latitude, double longitude,
        string address, string? logoUrl)
    {
        Check.NotEmpty(id, nameof(id));
        Check.NotEmpty(name, nameof(name));
        Check.NotEmpty(address, nameof(address));
        Check.NotNull(latitude, nameof(latitude));
        Check.NotNull(longitude, nameof(longitude));

        Id = id;
        Name = name;
        IsAvailable = false;
        IsOpen = false;
        IsDeleted = false;
        Location = new Location(latitude, longitude);
        Address = address;
        LogoUrl = logoUrl;
    }

    public void Open()
        => IsOpen = true;

    public void Close()
        => IsOpen = false;
}