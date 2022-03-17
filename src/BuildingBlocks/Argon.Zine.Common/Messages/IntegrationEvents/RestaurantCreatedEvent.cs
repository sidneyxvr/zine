namespace Argon.Zine.Commom.Messages.IntegrationEvents;

public record RestaurantCreatedEvent(Guid AggregateId, string Name, double Latitude, 
    double Longitude, string Address, string? LogoUrl) : Event(AggregateId);