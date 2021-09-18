using Argon.Zine.Core.DomainObjects;
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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Argon.Zine.Customers.Tests.Application.AddressHandlers
{
    public class DefineMainAddressHandlerTest
    {
        private readonly Faker _faker;
        private readonly AutoMocker _mocker;
        private readonly DefineMainAddressHandler _handler;
        private readonly CustomerFixture _customerFixture;
        private readonly IStringLocalizer<DefineMainAddressValidator> _localizer;

        public DefineMainAddressHandlerTest()
        {
            _faker = new Faker("pt_BR");
            _mocker = new AutoMocker();
            _handler = _mocker.CreateInstance<DefineMainAddressHandler>();
            _customerFixture = new CustomerFixture();

            _mocker.GetMock<IAppUser>()
                .Setup(a => a.Id)
                .Returns(Guid.NewGuid());

            _localizer = LocalizerHelper.CreateInstanceStringLocalizer<DefineMainAddressValidator>();
        }

        [Fact]
        public async Task DefineMainAddressShouldReturnValid()
        {
            //Arrange
            var customer = _customerFixture.CreateValidCustomerWithAddresses();
            var address = customer.Addresses
                .ElementAtOrDefault(_faker.Random.Int(0, customer.Addresses.Count - 1));

            var command = new DefineMainAddressCommand { AddressId = address.Id };

            _mocker.GetMock<IUnitOfWork>()
                .Setup(c => c.CustomerRepository
                    .GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Include>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(customer);

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
        public void ValidateCommand_Valid_ShouldReturnSuccess()
        {
            //Arrange
            var customer = _customerFixture.CreateValidCustomerWithAddresses();
            var address = customer.Addresses
                .ElementAtOrDefault(_faker.Random.Int(0, customer.Addresses.Count - 1));

            var command = new DefineMainAddressCommand { AddressId = address.Id };

            //Act
            var result = new DefineMainAddressValidator(_localizer).Validate(command);

            //Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void DeleteAddressShouldReturnInvalid()
        {
            //Arrange
            var command = new DefineMainAddressCommand { AddressId = Guid.Empty };

            //Act
            var result = new DefineMainAddressValidator(_localizer).Validate(command);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Equals("Informe o identificador do endereço"));
        }

        [Fact]
        public async Task DefineMainAddressShouldThrowNotFoundCustomer()
        {
            //Arrange
            var command = new DefineMainAddressCommand { AddressId = Guid.NewGuid() };

            _mocker.GetMock<IUnitOfWork>()
                .Setup(c => c.CustomerRepository
                    .GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Include>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Customer)null);

            //Act
            var result = await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _handler.Handle(command, CancellationToken.None));

            //Assert
            Assert.StartsWith("Customer cannot be null", result.Message);
        }

        [Fact]
        public async Task DefineMainAddressShouldThrowDomainExceptionAddress()
        {
            //Arrange
            var customer = _customerFixture.CreateValidCustomerWithAddresses();
            var address = customer.Addresses
                .ElementAtOrDefault(_faker.Random.Int(0, customer.Addresses.Count - 1));

            var command = new DefineMainAddressCommand { AddressId = Guid.NewGuid() };

            _mocker.GetMock<IUnitOfWork>()
                .Setup(c => c.CustomerRepository
                    .GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Include>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(customer);

            //Act
            var result = await Assert.ThrowsAsync<DomainException>(() =>
                _handler.Handle(command, CancellationToken.None));

            //Assert
            Assert.Equal(nameof(address), result.Message);
        }
    }
}
