using Argon.Core.Messages;
using System;

namespace Argon.Customers.Application.Commands
{
    public record DefineMainAddressCommand : Command
    {
        public Guid AddressId { get; init; }
    }
}
