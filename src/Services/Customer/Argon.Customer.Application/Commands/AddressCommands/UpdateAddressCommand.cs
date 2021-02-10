﻿using Argon.Core.Messages;
using Argon.Customers.Application.Commands.Validations.AddressValidations;
using System;

namespace Argon.Customers.Application.Commands.AddressCommands
{
    public class UpdateAddressCommand : Command
    {
        public Guid CustomerId { get; private set; }
        public Guid AddressId { get; private set; }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string District { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string PostalCode { get; private set; }
        public string Complement { get; private set; }
        public double? Latitude { get; private set; }
        public double? Longitude { get; private set; }

        public UpdateAddressCommand(Guid customerId, Guid addressId, string street, string number, 
            string district, string city, string state, string country, string postalCode, 
            string complement, double? latitude, double? longitude)
        {
            CustomerId = customerId;
            AddressId = addressId;
            Street = street;
            Number = number;
            District = district;
            City = city;
            State = state;
            Country = country;
            PostalCode = postalCode;
            Complement = complement;
            Latitude = latitude;
            Longitude = longitude;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateAddressValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
