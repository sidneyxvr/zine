using Argon.Core.Messages;
using System;

namespace Argon.Catalog.Communication.Events
{
    public record ProductCreatedEvent : Event
    {
        public string Name { get; init; } = null!;
        public decimal Price { get; init; }
        public string? ImageUrl { get; init; }
        public Guid RestaurantId { get; init; }
        public string RestaurantName { get; set; } = null!;
        public string? RestaurantLogo { get; set; }

        public ProductCreatedEvent(Guid aggregateId, string name, 
            decimal price, string? imageUrl, Guid restaurantId,
            string restaurantName, string? restaurantLogo)
        {
            AggregateId = aggregateId;
            Name = name;
            Price = price;
            ImageUrl = imageUrl;
            RestaurantId = restaurantId;
            RestaurantName = restaurantName;
            RestaurantLogo = restaurantLogo;
        }
    }
}