namespace Argon.Zine.Basket.Requests;

public record ProductToBasketRequest
{
    public Guid Id { get; set; }
    public int Amount { get; set; }
}

public record ProductToBasketDTO
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public string? ImageUrl { get; set; }
    public Guid RestaurantId { get; set; }
    public string RestaurantName { get; set; }
    public string? RestaurantLogoUrl { get; set; }

    public ProductToBasketDTO(Guid id, string name, decimal price,
        int amount, string? imageUrl, Guid restaurantId,
        string restaurantName, string? restaurantLogoUrl)
    {
        Id = id;
        Name = name;
        Price = price;
        Amount = amount;
        ImageUrl = imageUrl;
        RestaurantId = restaurantId;
        RestaurantName = restaurantName;
        RestaurantLogoUrl = restaurantLogoUrl;
    }
}