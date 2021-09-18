﻿using Argon.Zine.Core.Messages;
using System;

namespace Argon.Zine.Customers.Application.Commands;

public record DefineMainAddressCommand : Command
{
    public Guid AddressId { get; init; }
}