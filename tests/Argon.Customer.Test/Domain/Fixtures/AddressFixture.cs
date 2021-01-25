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
            var state = _faker.Address.State();
            var street = _faker.Address.StreetName();
            var number = _faker.Address.BuildingNumber();
            var district = _faker.Lorem.Sentence(50);
            var city = _faker.Address.City();
            var postalCode = _faker.Random.Int(00000000, 99999999).ToString();
            var complement = _faker.Lorem.Sentence(100);

            var address = new Address(street, number, district, city, state, country, postalCode, complement);

            return address;
        }
    }
}
