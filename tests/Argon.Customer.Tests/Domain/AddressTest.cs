using Argon.Core.DomainObjects;
using Argon.Customers.Domain;
using Argon.Customers.Tests.Fixtures;
using Bogus;
using System;
using Xunit;

namespace Argon.Customers.Tests.Domain
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
                new Address(Guid.NewGuid(), street, address.Number, address.District, address.City, address.State,
                address.PostalCode, address.Complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal(nameof(Address.Street), result.Message);
        }

        [Fact]
        public void CreateAddresssStreetOutOfRangeShouldThrowDomainException()
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();
            var street = _faker.Lorem.Letter(_faker.Random.Int(51, 100));

            //Act
            var result = Assert.Throws<DomainException>(() =>
                new Address(Guid.NewGuid(), street, address.Number, address.District, address.City, address.State,
                    address.PostalCode, address.Complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal(nameof(Address.Street), result.Message);
        }

        [Fact]
        public void CreateAddresssInvalidNumberShouldThrowDomainException()
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();
            var number = _faker.Lorem.Letter(_faker.Random.Int(Address.NumberMaxLength, Address.NumberMaxLength + 5));

            //Act
            var result = Assert.Throws<DomainException>(() =>
                new Address(Guid.NewGuid(), address.Street, number, address.District, address.City, address.State,
                    address.PostalCode, address.Complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal(nameof(Address.Number), result.Message);
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
                new Address(Guid.NewGuid(), address.Street, address.Number, district, address.City, address.State,
                    address.PostalCode, address.Complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal(nameof(Address.District), result.Message);
        }

        [Fact]
        public void CreateAddressDistrictOutOfRangeShouldThrowDomainException()
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();
            var district = _faker.Lorem.Letter(_faker.Random.Int(51, 100));

            //Act
            var result = Assert.Throws<DomainException>(() =>
                new Address(Guid.NewGuid(), address.Street, address.Number, district, address.City, address.State,
                    address.PostalCode, address.Complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal(nameof(Address.District), result.Message);
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
                new Address(Guid.NewGuid(), address.Street, address.Number, address.District, city, address.State,
                    address.PostalCode, address.Complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal(nameof(Address.City), result.Message);
        }

        [Fact]
        public void CreateAddressCityOutOfRangeShouldThrowDomainException()
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();
            var city = _faker.Lorem.Letter(_faker.Random.Int(41, 100));

            //Act
            var result = Assert.Throws<DomainException>(() =>
                new Address(Guid.NewGuid(), address.Street, address.Number, address.District, city, address.State,
                    address.PostalCode, address.Complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal(nameof(Address.City), result.Message);
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
                new Address(Guid.NewGuid(), address.Street, address.Number, address.District, address.City, state,
                    address.PostalCode, address.Complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal(nameof(Address.State), result.Message);
        }

        [Fact]
        public void CreateAddressInvalidStateShouldThrowDomainException()
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();
            var state = _faker.Lorem.Letter(_faker.Random.Int(3, 100));

            //Act
            var result = Assert.Throws<DomainException>(() =>
                new Address(Guid.NewGuid(), address.Street, address.Number, address.District, address.City, state,
                    address.PostalCode, address.Complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal(nameof(Address.State), result.Message);
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
                new Address(Guid.NewGuid(), address.Street, address.Number, address.District, address.City, address.State,
                postalCode, address.Complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal(nameof(Address.PostalCode), result.Message);
        }

        [Fact]
        public void CreateAddressComplementOutOfRangeShouldThrowDomainException()
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();
            var complement = _faker.Lorem.Letter(_faker.Random.Int(51, 100));

            //Act
            var result = Assert.Throws<DomainException>(() =>
                new Address(Guid.NewGuid(), address.Street, address.Number, address.District, address.City, address.State,
                    address.PostalCode, complement, address.Latitude, address.Longitude));

            //Assert
            Assert.Equal(nameof(Address.Complement), result.Message);
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
                new Address(Guid.NewGuid(), address.Street, address.Number, address.District, address.City, address.State,
                    address.PostalCode, address.Complement, latitude, address.Longitude));

            //Assert
            Assert.Equal(nameof(Address.Location.Latitude).ToLower(), result.Message);
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
                new Address(Guid.NewGuid(), address.Street, address.Number, address.District, address.City, address.State,
                    address.PostalCode, address.Complement, address.Latitude, longitude));

            //Assert
            Assert.Equal(nameof(Address.Location.Longitude).ToLower(), result.Message);
        }

        [Fact]
        public void CreateValidAddressShouldWorkSuccessfully()
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();

            //Act
            var result = new Address(Guid.NewGuid(), address.Street, address.Number, address.District, address.City, 
                address.State, address.PostalCode, address.Complement, address.Latitude, address.Longitude);

            //Assert
            Assert.Equal(address.Street, result.Street);
            Assert.Equal(address.Number, result.Number);
            Assert.Equal(address.District, result.District);
            Assert.Equal(address.City, result.City);
            Assert.Equal(address.State, result.State);
            Assert.Equal(address.PostalCode, result.PostalCode);
            Assert.Equal(address.Complement, result.Complement);
        }

        [Fact]
        public void CreateAddressNullCoordinatesShouldWorkSuccessfully()
        {
            //Arrange
            var address = _addressFixture.GetAddressTestDTO();

            //Act
            var result = new Address(Guid.NewGuid(), address.Street, address.Number, address.District, 
                address.City, address.State, address.PostalCode, address.Complement, null, null);

            //Assert
            Assert.Null(result.Location);
        }

        [Fact]
        public void UpdateAddressShouldUpdate()
        {
            //Arrange
            var street = "Rua nome 1";
            var number = "12345";
            var district = "Bairro 1";
            var city = "Cidade 1";
            var state = "CE";
            var country = "Brasil";
            var postalCode = "50000999";

            //Act
            var result = new Address(Guid.NewGuid(), street, number, district, city, state, postalCode, null, null, null);

            //Assert
            Assert.Equal(street, result.Street);
            Assert.Equal(number, result.Number);
            Assert.Equal(district, result.District);
            Assert.Equal(city, result.City);
            Assert.Equal(state, result.State);
            Assert.Equal(country, result.Country);
            Assert.Equal(postalCode, result.PostalCode);
        }
    }
}
