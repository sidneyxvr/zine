using Argon.Zine.Core.Messages;

namespace Argon.Customers.Application.Commands
{
    public record DefineMainAddressCommand : Command
    {
        public Guid AddressId { get; init; }
    }
}
