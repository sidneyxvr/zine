using System;

namespace Argon.Core.Messages.IntegrationEvents
{
    public record SupplierCreatedEvent : Event
    {
        public Guid Id { get; set; }
        public string? Name { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public string? Address { get; init; }
    }
}
