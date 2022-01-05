using Argon.Zine.Commom.DomainObjects;
using Argon.Zine.Customers.Application.Commands;
using Argon.Zine.Customers.Application.Handlers;
using Argon.Zine.Customers.Application.Validators;
using Argon.Zine.Customers.Domain;
using Argon.Zine.Customers.Tests.Fixtures;
using Bogus;
using Microsoft.Extensions.Localization;
using Moq;
using Moq.AutoMock;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Argon.Zine.Customers.Tests.Application.AddressHandlers;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods", Justification = "<Pending>")]
public class CreateAddressHandlerTest
{
    private readonly Faker _faker;
    private readonly AutoMocker _mocker;
    private readonly CreateAddressHandler _handler;
    private readonly AddressFixture _addressFixture;
    private readonly CustomerFixture _customerFixture;
    private readonly IStringLocalizer<CreateAddressValidator> _localizer;

    public CreateAddressHandlerTest()
    {
        _faker = new Faker("pt_BR");
        _mocker = new AutoMocker();
        _handler = _mocker.CreateInstance<CreateAddressHandler>();
        _addressFixture = new AddressFixture();
        _customerFixture = new CustomerFixture();

        _localizer = LocalizerHelper.CreateInstanceStringLocalizer<CreateAddressValidator>();

        _mocker.GetMock<IAppUser>()
            .Setup(a => a.Id)
            .Returns(Guid.NewGuid());
    }

    [Fact]
    public async Task CreateAddressShouldCreate()
    {
        //Arrange
        var properties = _addressFixture.GetAddressTestDTO();

        var customer = _customerFixture.CreateValidCustomer();

        var command = new CreateAddressCommand
        {
            Street = properties.Street,
            Number = properties.Number,
            District = properties.District,
            City = properties.City,
            State = properties.State,
            PostalCode = properties.PostalCode,
            Complement = properties.Complement,
            Latitude = properties.Latitude,
            Longitude = properties.Longitude,
        };

        _mocker.GetMock<IUnitOfWork>()
            .Setup(c => c.CustomerRepository
                .GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Includes>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(_customerFixture.CreateValidCustomerWithAddresses());

        _mocker.GetMock<IUnitOfWork>()
            .Setup(u => u.CustomerRepository.UpdateAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()));

        _mocker.GetMock<IUnitOfWork>()
            .Setup(u => u.CommitAsync())
            .ReturnsAsync(true);

        //Act
        var result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.True(result.IsValid);
        _mocker.GetMock<IUnitOfWork>().Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public void CreateAddressShouldReturnInvalid()
    {
        //Arrange
        var command = new CreateAddressCommand
        {
            Street = "",
            Number = "",
            District = "",
            City = "",
            State = "",
            PostalCode = "",
            Complement = "",
        };

        //Act
        var result = new CreateAddressValidator(_localizer).Validate(command);

        //Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void CreateAddressNullFieldsShouldReturnInvalidWithErrorList()
    {
        //Arrange
        var command = new CreateAddressCommand
        {
        };

        //Act
        var result = new CreateAddressValidator(_localizer).Validate(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal(5, result.Errors.Count);
        Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe a cidade"));
        Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe o bairro"));
        Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe a rua"));
        Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe o estado"));
        Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe o CEP"));
    }

    [Fact]
    public void CreateAddressEmptyFielsShouldReturnInvalidWithErrorList()
    {
        //Arrange
        var command = new CreateAddressCommand
        {
            Street = "",
            Number = _faker.Random.ULong(10_000_000_000).ToString(),
            District = "",
            City = "",
            State = "",
            PostalCode = "",
            Complement = _faker.Lorem.Letter(_faker.Random.Int(51, 100)),
        };

        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("pt-BR");

        //Act
        var result = new CreateAddressValidator(_localizer).Validate(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal(7, result.Errors.Count);
        Assert.Contains(result.Errors, a => a.ErrorMessage.Equals($"A cidade deve ter entre {Address.CityMinLength} e {Address.CityMaxLength} caracteres"));
        Assert.Contains(result.Errors, a => a.ErrorMessage.Equals($"O bairro deve ter entre {Address.DistrictMinLength} e {Address.DistrictMaxLength} caracteres"));
        Assert.Contains(result.Errors, a => a.ErrorMessage.Equals($"A rua deve ter entre {Address.StreetMinLength} e {Address.StreetMaxLength} caracteres"));
        Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Estado inválido"));
        Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("CEP inválido"));
        Assert.Contains(result.Errors, a => a.ErrorMessage.Equals($"O número deve ter no máximo {Address.NumberMaxLength} caracteres"));
        Assert.Contains(result.Errors, a => a.ErrorMessage.Equals($"O complemento deve ter no máximo {Address.ComplementMaxLength} caracteres"));
    }

    [Fact]
    public void CreateAddressInvalidCoordinateShouldReturnInvalidWithErrorList()
    {
        //Arrange
        var properties = _addressFixture.GetAddressTestDTO();

        var command = new CreateAddressCommand
        {
            Street = properties.Street,
            Number = properties.Number,
            District = properties.District,
            City = properties.City,
            State = properties.State,
            PostalCode = properties.PostalCode,
            Latitude = 91,
            Longitude = 181,
        };

        //Act
        var result = new CreateAddressValidator(_localizer).Validate(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal(2, result.Errors.Count);
        Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Latitude inválida"));
        Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Longitude inválida"));
    }


    [Fact]
    public void CreateAddressLatitudeNullShouldReturnInvalid()
    {
        //Arrange
        var properties = _addressFixture.GetAddressTestDTO();

        var command = new CreateAddressCommand
        {
            Street = properties.Street,
            Number = properties.Number,
            District = properties.District,
            City = properties.City,
            State = properties.State,
            PostalCode = properties.PostalCode,
            Longitude = properties.Longitude,
        };

        //Act
        var result = new CreateAddressValidator(_localizer).Validate(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Coordenadas inválidas"));
    }
}