using Argon.Zine.Core.Messages;
using System;

namespace Argon.Restaurants.Application.Commands;

public record UpdateAddressCommand : Command
{
    public Guid AddressId { get; init; }
    public string Street { get; init; } = null!;
    public string Number { get; init; } = null!;
    public string District { get; init; } = null!;
    public string City { get; init; } = null!;
    public string State { get; init; } = null!;
    public string PostalCode { get; init; } = null!;
    public string Complement { get; init; } = null!;
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
}