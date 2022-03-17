using Argon.Zine.Commom.Messages;

namespace Argon.Zine.Catalog.Communication.Events;

public record ProductCreatedEvent(Guid AggregateId, string Name, decimal Price, string? ImageUrl, 
    Guid RestaurantId, string RestaurantName, string? RestaurantLogo) : Event(AggregateId);