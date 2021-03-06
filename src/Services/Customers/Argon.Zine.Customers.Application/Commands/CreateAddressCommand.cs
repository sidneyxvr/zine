using Argon.Zine.Commom.Messages;

namespace Argon.Zine.Customers.Application.Commands;

#pragma warning disable CS8618
public record CreateAddressCommand : Command
{
    public string Street { get; init; }
    public string? Number { get; init; }
    public string District { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string PostalCode { get; init; }
    public string? Complement { get; init; }
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
}
#pragma warning restore CS8618