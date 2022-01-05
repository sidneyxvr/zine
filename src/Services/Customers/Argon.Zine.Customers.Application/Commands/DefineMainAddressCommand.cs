using Argon.Zine.Commom.Messages;

namespace Argon.Zine.Customers.Application.Commands;

public record DefineMainAddressCommand : Command
{
    public Guid AddressId { get; init; }
}