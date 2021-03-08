﻿using Argon.Core.Messages;
using Argon.Customers.Application.Commands.Validators.AddressValidators;
using System;

namespace Argon.Customers.Application.Commands.AddressCommands
{
    public class DefineMainAddressCommand : Command
    {
        public Guid CustomerId { get; init; }
        public Guid AddressId { get; init; }

        public override bool IsValid()
        {
            ValidationResult = new DefineMainAddressValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}