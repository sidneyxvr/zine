using Argon.Zine.Core.Messages;

namespace Argon.Zine.Customers.Application.Commands;

public record DeleteAddressCommand : Command
{
    public Guid AddressId { get; init; }
}