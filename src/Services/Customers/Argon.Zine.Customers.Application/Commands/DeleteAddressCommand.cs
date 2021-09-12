using Argon.Zine.Core.Messages;
using System;

namespace Argon.Zine.Customers.Application.Commands
{
    public record DeleteAddressCommand : Command
    {
        public Guid AddressId { get; init; }
    }
}
