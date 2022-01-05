using Argon.Zine.Commom.DomainObjects;

namespace Argon.Zine.Ordering.Domain;

public class Restaurant : Entity
{
    public const int NameMaxLength = 50;

    public string Name { get; private set; }
    public string? LogoUrl { get; private set; }

#pragma warning disable CS8618
    private Restaurant() { }
#pragma warning restore CS8618

    public Restaurant(Guid id, string name, string? logoUrl)
    {
        Id = id;
        Name = name;
        LogoUrl = logoUrl;
    }
}