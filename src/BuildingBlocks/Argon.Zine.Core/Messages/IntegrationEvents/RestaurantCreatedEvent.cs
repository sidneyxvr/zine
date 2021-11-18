namespace Argon.Zine.Core.Messages.IntegrationEvents;

public record RestaurantCreatedEvent : Event
{
    public string Name { get; private init; }
    public double Latitude { get; private init; }
    public double Longitude { get; private init; }
    public string Address { get; private init; }
    public string? LogoUrl { get; private init; }

    public RestaurantCreatedEvent(Guid aggregateId, string name,
        double latitude, double longitude, string address, string? logoUrl)
    {
        AggregateId = aggregateId;
        Name = name;
        Latitude = latitude;
        Longitude = longitude;
        Address = address;
        LogoUrl = logoUrl;
    }
}