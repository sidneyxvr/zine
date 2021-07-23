using System;

namespace Argon.Catalog.QueryStack.Response
{
    public record ProductBasketResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
        public decimal Price { get; init; }
        public string? ImageUrl { get; init; } = null!;
        public Guid RestaurantId { get; init; }
        public string RestaurantName { get; init; } = null!;
        public string? RestaurantLogoUrl { get; init; } = null!;
    }
}
