﻿using Argon.Core.Messages;
using Argon.Suppliers.Application.Commands.Validators;
using System;

namespace Argon.Suppliers.Application.Commands
{
    public class UpdateAddressCommand : Command
    {
        public Guid AddressId { get; init; }
        public string Street { get; init; }
        public string Number { get; init; }
        public string District { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string PostalCode { get; init; }
        public string Complement { get; init; }
        public double? Latitude { get; init; }
        public double? Longitude { get; init; }

        public override bool IsValid()
        {
            ValidationResult = new UpdateAddressValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
