using Argon.Zine.Commom.DomainObjects;
using Argon.Zine.Customers.Domain;
using Argon.Zine.Customers.Tests.Fixtures;
using Bogus;
using System;
using Xunit;

namespace Argon.Zine.Customers.Tests.Domain;

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
            address.PostalCode, new(address.Latitude, address.Longitude)));

        //Assert
        Assert.Equal(nameof(Address.Street).ToLower(), result.Message);
    }

    [Fact]
    public void CreateAddresssStreetOutOfRangeShouldThrowDomainException()
    {
        //Arrange
        var address = _addressFixture.GetAddressTestDTO();
        var street = _faker.Lorem.Letter(_faker.Random.Int(51, 100));

        //Act
        var result = Assert.Throws<DomainException>(() =>
            new Address( street, address.Number, address.District, address.City, address.State,
                address.PostalCode, new(address.Latitude, address.Longitude)));

        //Assert
        Assert.Equal(nameof(Address.Street).ToLower(), result.Message);
    }

    [Fact]
    public void CreateAddresssInvalidNumberShouldThrowDomainException()
    {
        //Arrange
        var address = _addressFixture.GetAddressTestDTO();
        var number = _faker.Lorem.Letter(_faker.Random.Int(Address.NumberMaxLength, Address.NumberMaxLength + 5));

        //Act
        var result = Assert.Throws<DomainException>(() =>
            new Address(address.Street, number, address.District, address.City, address.State,
                address.PostalCode, new(address.Latitude, address.Longitude)));

        //Assert
        Assert.Equal(nameof(Address.Number).ToLower(), result.Message);
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
                address.PostalCode, new(address.Latitude, address.Longitude)));

        //Assert
        Assert.Equal(nameof(Address.District).ToLower(), result.Message);
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
                address.PostalCode, new(address.Latitude, address.Longitude)));

        //Assert
        Assert.Equal(nameof(Address.District).ToLower(), result.Message);
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
                address.PostalCode, new(address.Latitude, address.Longitude)));

        //Assert
        Assert.Equal(nameof(Address.City).ToLower(), result.Message);
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
                address.PostalCode, new(address.Latitude, address.Longitude)));

        //Assert
        Assert.Equal(nameof(Address.City).ToLower(), result.Message);
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
                address.PostalCode, new(address.Latitude, address.Longitude)));

        //Assert
        Assert.Equal(nameof(Address.State).ToLower(), result.Message);
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
                address.PostalCode, new(address.Latitude, address.Longitude)));

        //Assert
        Assert.Equal(nameof(Address.State).ToLower(), result.Message);
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
            postalCode, new(address.Latitude, address.Longitude)));

        //Assert
        Assert.Equal("postalCode", result.Message);
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
                address.PostalCode, new(latitude, address.Longitude)));

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
            new Address(address.Street, address.Number, address.District, address.City, address.State,
                address.PostalCode, new(address.Latitude, longitude)));

        //Assert
        Assert.Equal(nameof(Address.Location.Longitude).ToLower(), result.Message);
    }

    [Fact]
    public void CreateValidAddressShouldWorkSuccessfully()
    {
        //Arrange
        var address = _addressFixture.GetAddressTestDTO();

        //Act
        var result = new Address(address.Street, address.Number, address.District, address.City,
            address.State, address.PostalCode, new(address.Latitude, address.Longitude));

        //Assert
        Assert.Equal(address.Street, result.Street);
        Assert.Equal(address.Number, result.Number);
        Assert.Equal(address.District, result.District);
        Assert.Equal(address.City, result.City);
        Assert.Equal(address.State, result.State);
        Assert.Equal(address.PostalCode, result.PostalCode);
    }
}