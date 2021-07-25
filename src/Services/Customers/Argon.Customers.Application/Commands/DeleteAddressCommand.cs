using Argon.Core.Messages;
using System;

namespace Argon.Customers.Application.Commands
{
    public record DeleteAddressCommand : Command
    {
        public Guid AddressId { get; init; }
    }
}
