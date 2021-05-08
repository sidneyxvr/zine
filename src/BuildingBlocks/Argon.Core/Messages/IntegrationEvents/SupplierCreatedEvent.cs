namespace Argon.Core.Messages.IntegrationEvents
{
    public class SupplierCreatedEvent : Event
    {
        public string Name { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public string Address { get; init; }
    }
}
