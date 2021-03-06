namespace Argon.Zine.Basket.Requests;

public record ProductToBasketRequest(Guid ProductId, int Amount);

public record ProductToBasketDto(
    Guid Id,
    string Name,
    decimal Price,
    int Amount,
    string? ImageUrl,
    Guid RestaurantId,
    string RestaurantName,
    string? RestaurantLogoUrl);