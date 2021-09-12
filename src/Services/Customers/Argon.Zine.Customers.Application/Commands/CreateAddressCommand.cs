using Argon.Zine.Core.Messages;

namespace Argon.Zine.Customers.Application.Commands
{
    public record CreateAddressCommand : Command
    {
        public string Street { get; init; } = null!;
        public string Number { get; init; } = null!;
        public string District { get; init; } = null!;
        public string City { get; init; } = null!;
        public string State { get; init; } = null!;
        public string PostalCode { get; init; } = null!;
        public string? Complement { get; init; }
        public double? Latitude { get; init; }
        public double? Longitude { get; init; }
    }
}
