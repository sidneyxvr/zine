using System;

namespace Argon.WebApi.API.Requests
{
    public class AddressRequest
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Complement { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public Guid CustomerId { get; set; }
    }
}
