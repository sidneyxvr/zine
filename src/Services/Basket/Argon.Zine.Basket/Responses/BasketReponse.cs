namespace Argon.Zine.Basket.Responses;

public record BasketResponse(
    Guid RestaurantId,
    string RestaurantName,
    string? RestaurantLogoUrl,
    decimal Total,
    IEnumerable<ProductDto> Products);

public record ProductDto(
    Guid Id,
    string Name,
    int Amount,
    decimal Price,
    string? ImageUrl);