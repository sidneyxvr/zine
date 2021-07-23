using System;

namespace Argon.Catalog.QueryStack.Response
{
    public record RestaurantDetails
    {
        public Guid Id { get; init; }
        public string Nome { get; init; } = null!;
    }
}
