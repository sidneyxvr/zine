using Argon.Zine.Commom.DomainObjects;
using Argon.Zine.Customers.Application.Commands;
using Argon.Zine.Customers.Application.Handlers;
using Argon.Zine.Customers.Domain;
using Argon.Zine.Customers.Tests.Fixtures;
using Bogus;
using Moq;
using Moq.AutoMock;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Argon.Zine.Customers.Tests.Application.AddressHandlers;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods", Justification = "<Pending>")]
public class DeleteAddressHandlerTest
{
    private readonly Faker _faker;
    private readonly AutoMocker _mocker;
    private readonly DeleteAddressHandler _handler;
    private readonly CustomerFixture _customerFixture;

    public DeleteAddressHandlerTest()
    {
        _faker = new Faker("pt_BR");
        _mocker = new AutoMocker();
        _handler = _mocker.CreateInstance<DeleteAddressHandler>();
        _customerFixture = new CustomerFixture();
    }

    [Fact]
    public async Task DeleteAddressShouldReturnValid()
    {
        //Arrange
        var customer = _customerFixture.CreateValidCustomerWithAddresses();
        var address = customer.Addresses
            .ElementAtOrDefault(_faker.Random.Int(0, customer.Addresses.Count - 1));

        var command = new DeleteAddressCommand { AddressId = address.Id };

        _mocker.GetMock<IUnitOfWork>()
            .Setup(c => c.CustomerRepository
                .GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Includes>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        _mocker.GetMock<IUnitOfWork>()
            .Setup(u => u.CommitAsync())
            .ReturnsAsync(true);

        //Act
        var result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.True(result.ValidationResult.IsValid);
        _mocker.GetMock<IUnitOfWork>().Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAddressShouldThrowNotFoundCustomer()
    {
        //Arrange
        var command = new DeleteAddressCommand { AddressId = Guid.NewGuid() };

        _mocker.GetMock<IAppUser>()
            .Setup(a => a.Id).Returns(Guid.NewGuid());

        _mocker.GetMock<IUnitOfWork>()
            .Setup(c => c.CustomerRepository
                .GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Includes>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer)null);

        _mocker.GetMock<IUnitOfWork>()
            .Setup(c => c.CustomerRepository.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Includes>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer)null);

        //Act
        var result = await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _handler.Handle(command, CancellationToken.None));

        //Assert
        Assert.StartsWith("Customer cannot be null", result.Message);
    }
}