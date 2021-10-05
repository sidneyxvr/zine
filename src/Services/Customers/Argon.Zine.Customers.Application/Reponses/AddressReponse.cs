﻿namespace Argon.Zine.Customers.Application.Reponses
{
    public class AddressReponse
    {
        public string Street { get; init; } = null!;
        public string Number { get; init; } = null!;
        public string District { get; init; } = null!;
        public string City { get; init; } = null!;
        public string State { get; init; } = null!;
        public string Country { get; init; } = null!;
        public string PostalCode { get; init; } = null!;
        public string? Complement { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
    }
}