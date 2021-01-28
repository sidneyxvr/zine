using Argon.Customers.Domain.AggregatesModel.CustomerAggregate;
using Bogus;

namespace Argon.Customers.Test.Domain.Fixtures
{
    public class AddressFixture
    {
        private readonly Faker _faker;
        public AddressFixture()
        {
            _faker = new Faker("pt_BR");
        }

        public Address CreateValidAddress()
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

            return new Address(street, number, district, city, state, country, postalCode, complement, latitude, longitude);
        }
    }
}
