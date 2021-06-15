using Argon.Core.Messages;
using System;

namespace Argon.Customers.Application
{
    public class DefineMainAddressCommand : Command
    {
        public Guid CustomerId { get; init; }
        public Guid AddressId { get; init; }
    }
}
