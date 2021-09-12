using System;

namespace Argon.Zine.Catalog.QueryStack.Response
{
    public record ProductDetailsResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
        public decimal Price { get; init; }
        public string? ImageUrl { get; init; }
    }
}
