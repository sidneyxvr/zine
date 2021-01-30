using Argon.Core.DomainObjects;
using Argon.Customers.Domain.AggregatesModel.CustomerAggregate;
using Argon.Customers.Test.Domain.Fixtures;
using Bogus;
using Xunit;

namespace Argon.Customers.Test.Domain
{
    public class AddressTest
    {
        private readonly Faker _faker;
        private readonly AddressFixture _addressFixture;

        public AddressTest()
        {
            _faker = new Faker("pt_BR");
            _addressFixture = new AddressFixture();
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void CreateAddresssEmptyStreetShouldThrowDomainException(string street)
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();

            //Act
            var result = Assert.Throws<DomainException>(() =>
                new Address(street, address.Number, address.District, address.City, address.State,
                address.Country, address.PostalCode, address.Complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal("Informe a rua", result.Message);
        }

        [Fact]
        public void CreateAddresssStreetOutOfRangeShouldThrowDomainException()
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();
            var street = _faker.Lorem.Letter(_faker.Random.Int(51, 100));

            //Act
            var result = Assert.Throws<DomainException>(() =>
                new Address(street, address.Number, address.District, address.City, address.State,
                address.Country, address.PostalCode, address.Complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal("A rua deve ter entre 2 e 50 caracteres", result.Message);
        }

        [Fact]
        public void CreateAddresssInvalidNumberShouldThrowDomainException()
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();
            var number = _faker.Lorem.Letter(_faker.Random.Int(6, 10));

            //Act
            var result = Assert.Throws<DomainException>(() =>
                new Address(address.Street, number, address.District, address.City, address.State,
                address.Country, address.PostalCode, address.Complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal("O número deve ter no máximo 5 caracteres", result.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void CreateAddresssEmptyDistrictShouldThrowDomainException(string district)
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();

            //Act
            var result = Assert.Throws<DomainException>(() =>
                new Address(address.Street, address.Number, district, address.City, address.State,
                address.Country, address.PostalCode, address.Complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal("Informe o bairro", result.Message);
        }

        [Fact]
        public void CreateAddressDistrictOutOfRangeShouldThrowDomainException()
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();
            var district = _faker.Lorem.Letter(_faker.Random.Int(51, 100));

            //Act
            var result = Assert.Throws<DomainException>(() =>
                new Address(address.Street, address.Number, district, address.City, address.State,
                address.Country, address.PostalCode, address.Complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal("O bairro deve ter entre 2 e 50 caracteres", result.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void CreateAddressEmptyCityShouldThrowDomainException(string city)
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();

            //Act
            var result = Assert.Throws<DomainException>(() =>
                new Address(address.Street, address.Number, address.District, city, address.State,
                address.Country, address.PostalCode, address.Complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal("Informe a cidade", result.Message);
        }

        [Fact]
        public void CreateAddressCityOutOfRangeShouldThrowDomainException()
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();
            var city = _faker.Lorem.Letter(_faker.Random.Int(41, 100));

            //Act
            var result = Assert.Throws<DomainException>(() =>
                new Address(address.Street, address.Number, address.District, city, address.State,
                address.Country, address.PostalCode, address.Complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal("A cidade deve ter entre 2 e 40 caracteres", result.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void CreateAddressEmptyStateShouldThrowDomainException(string state)
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();

            //Act
            var result = Assert.Throws<DomainException>(() =>
                new Address(address.Street, address.Number, address.District, address.City, state,
                address.Country, address.PostalCode, address.Complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal("Informe o estado", result.Message);
        }

        [Fact]
        public void CreateAddressInvalidStateShouldThrowDomainException()
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();
            var state = _faker.Lorem.Letter(_faker.Random.Int(3, 100));

            //Act
            var result = Assert.Throws<DomainException>(() =>
                new Address(address.Street, address.Number, address.District, address.City, state,
                address.Country, address.PostalCode, address.Complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal("Estado inválido", result.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void CreateAddressEmptyCountryShouldThrowDomainException(string country)
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();

            //Act
            var result = Assert.Throws<DomainException>(() =>
                new Address(address.Street, address.Number, address.District, address.City, address.State,
                country, address.PostalCode, address.Complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal("Informe o país", result.Message);
        }

        [Fact]
        public void CreateAddressCountryOutOfRangeShouldThrowDomainException()
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();
            var country = _faker.Lorem.Letter(_faker.Random.Int(51, 100));

            //Act
            var result = Assert.Throws<DomainException>(() =>
                new Address(address.Street, address.Number, address.District, address.City, address.State,
                country, address.PostalCode, address.Complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal("O país deve ter entre 2 e 50 caracteres", result.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void CreateAddressEmptyPostalCodeShouldThrowDomainException(string postalCode)
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();

            //Act
            var result = Assert.Throws<DomainException>(() =>
                new Address(address.Street, address.Number, address.District, address.City, address.State,
                address.Country, postalCode, address.Complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal("Informe o CEP", result.Message);
        }

        [Fact]
        public void CreateAddressInvalidPostalCodeShouldThrowDomainException()
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();
            var country = _faker.Lorem.Letter(_faker.Random.Int(51, 100));

            //Act
            var result = Assert.Throws<DomainException>(() =>
                new Address(address.Street, address.Number, address.District, address.City, address.State,
                country, address.PostalCode, address.Complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal("O país deve ter entre 2 e 50 caracteres", result.Message);
        }

        [Fact]
        public void CreateAddressComplementOutOfRangeShouldThrowDomainException()
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();
            var complement = _faker.Lorem.Letter(_faker.Random.Int(51, 100));

            //Act
            var result = Assert.Throws<DomainException>(() =>
                new Address(address.Street, address.Number, address.District, address.City, address.State,
                address.Country, address.PostalCode, complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal("O complemento deve ter no máximo 50 caracteres", result.Message);
        }

        [Fact]
        public void CreateAddressInvalidLatitudeShouldThrowDomainException()
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();
            var greaterThanPositive90 = _faker.Random.Double(91, 1e9);
            var lessThanNegative90 = _faker.Random.Double(-1e9, -91);
            var latitude = _faker.Random.Bool() ? greaterThanPositive90 : lessThanNegative90;
            //Act
            var result = Assert.Throws<DomainException>(() =>
                new Address(address.Street, address.Number, address.District, address.City, address.State,
                address.Country, address.PostalCode, address.Complement, latitude, address.Longitude));

            //Assert
            Assert.Equal("Latitude inválida", result.Message);
        }

        [Fact]
        public void CreateAddressInvalidLongitudeShouldThrowDomainException()
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();
            var greaterThanPositive180 = _faker.Random.Double(181, 1e9);
            var lessThanNegative180 = _faker.Random.Double(-1e9, -181);
            var longitude = _faker.Random.Bool() ? greaterThanPositive180 : lessThanNegative180;
            //Act
            var result = Assert.Throws<DomainException>(() =>
                new Address(address.Street, address.Number, address.District, address.City, address.State,
                address.Country, address.PostalCode, address.Complement, address.Latitude, longitude));

            //Assert
            Assert.Equal("Longitude inválida", result.Message);
        }

        [Fact]
        public void CreateValidAddressShouldWorkSuccessfully()
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();

            //Act
            var result = new Address(address.Street, address.Number, address.District, address.City, address.State,
                address.Country, address.PostalCode, address.Complement, address.Latitude, address.Longitude);

            //Assert
            Assert.Equal(address.Street, result.Street);
            Assert.Equal(address.Number, result.Number);
            Assert.Equal(address.District, result.District);
            Assert.Equal(address.City, result.City);
            Assert.Equal(address.State, result.State);
            Assert.Equal(address.Country, result.Country);
            Assert.Equal(address.PostalCode, result.PostalCode);
            Assert.Equal(address.Complement, result.Complement);

            Assert.Equal(address.Latitude, result.Location.Latitude);
            Assert.Equal(address.Longitude, result.Location.Longitude);
        }

        [Fact]
        public void CreateAddressNullCoordinatesShouldWorkSuccessfully()
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();

            //Act
            var result = new Address(address.Street, address.Number, address.District, address.City, 
                address.State, address.Country, address.PostalCode, address.Complement, null, null);

            //Assert
            Assert.Null(result.Location);
        }
    }
}
