using Argon.Zine.Commom.DomainObjects;

namespace Argon.Zine.Catalog.Domain;

public class Product : Entity, IAggregateRoot
{
    public const int NameMaxLength = 100;
    public const int DescriptionMaxLength = 255;

    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsDeleted { get; private set; }
    public Guid RestaurantId { get; private set; }
    public Restaurant? Restaurant { get; private set; }
    public string ImageUrl { get; private set; }

#pragma warning disable CS8618
    protected Product() { }
#pragma warning restore CS8618
    public Product(string? name, string? description,
        decimal price, bool active, string? imageUrl, Guid restaurantId)
    {
        Check.NotEmpty(name, nameof(name));
        Check.NotEmpty(description, nameof(description));
        Check.Range(price, decimal.Zero, decimal.MaxValue, nameof(price));
        Check.NotEmpty(restaurantId, nameof(restaurantId));
        Check.NotEmpty(imageUrl, nameof(imageUrl));

        Name = name!;
        Description = description!;
        Price = price;
        RestaurantId = restaurantId;
        ImageUrl = imageUrl!;
        IsActive = active;
    }
}