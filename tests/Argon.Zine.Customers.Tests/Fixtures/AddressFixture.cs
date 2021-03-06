using Argon.Zine.Customers.Domain;
using Bogus;
using System;

namespace Argon.Zine.Customers.Tests.Fixtures;

public class AddressFixture
    {
        private readonly Faker _faker;
        public AddressFixture()
        {
            _faker = new Faker("pt_BR");
        }

        public AddressTestDTO GetAddressTestDTO()
        {
            var country = _faker.Address.Country();
            var state = _faker.Address.StateAbbr();
            var street = _faker.Address.StreetName();
            var number = _faker.Address.BuildingNumber();
            var district = _faker.Lorem.Letter(_faker.Random.Int(2, 50));
            var city = _faker.Address.City();
            var postalCode = _faker.Address.ZipCode("########");
            var complement = _faker.Lorem.Letter(_faker.Random.Int(2, 50));

            var latitude = _faker.Address.Latitude();
            var longitude = _faker.Address.Longitude();

            return new(Guid.NewGuid(), street, number, district, 
                city, state, country, postalCode, complement, latitude, longitude);
        }

        public Address CreateValidAddress()
        {
            var state = _faker.Address.StateAbbr();
            var street = _faker.Address.StreetName();
            var number = _faker.Address.BuildingNumber();
            var district = _faker.Lorem.Letter(_faker.Random.Int(2, 50));
            var city = _faker.Address.City();
            var postalCode = _faker.Address.ZipCode("########");

            var latitude = _faker.Address.Latitude();
            var longitude = _faker.Address.Longitude();

            return new(street, number, district, city, state, 
                postalCode, new(latitude, longitude));
        }
    }

    public record AddressTestDTO(Guid Id, string Street, string Number, 
        string District, string City, string State, string Country, 
        string PostalCode, string Complement, double Latitude, double Longitude);
