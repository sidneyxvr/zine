namespace Argon.Zine.Basket.Responses;

public record BasketReponse
{
    public Guid RestaurantId { get; init; }
    public string RestaurantName { get; init; } = null!;
    public string? RestaurantLogoUrl { get; init; }
    public decimal Total { get; init; }
    public IEnumerable<ProductDTO> Products { get; init; } = null!;
}

public record ProductDTO
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public int Amount { get; init; }
    public decimal Price { get; init; }
    public string ImageUrl { get; init; } = null!;
}
