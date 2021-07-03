using Argon.Core.Messages;
using System;

namespace Argon.Suppliers.Application.Commands
{
    public record UpdateAddressCommand : Command
    {
        public Guid AddressId { get; init; }
        public string? Street { get; init; }
        public string? Number { get; init; }
        public string? District { get; init; }
        public string? City { get; init; }
        public string? State { get; init; }
        public string? PostalCode { get; init; }
        public string? Complement { get; init; }
        public double? Latitude { get; init; }
        public double? Longitude { get; init; }
    }
}
