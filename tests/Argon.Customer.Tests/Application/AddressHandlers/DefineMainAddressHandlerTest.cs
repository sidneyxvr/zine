using Argon.Core.DomainObjects;
using Argon.Customers.Application;
using Argon.Customers.Domain;
using Argon.Customers.Tests.Fixtures;
using Bogus;
using Moq;
using Moq.AutoMock;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Argon.Customers.Tests.Application.AddressHandlers
{
    public class DefineMainAddressHandlerTest
    {
        private readonly Faker _faker;
        private readonly AutoMocker _mocker;
        private readonly DefineMainAddressHandler _handler;
        private readonly CustomerFixture _customerFixture;

        public DefineMainAddressHandlerTest()
        {
            _faker = new Faker("pt_BR");
            _mocker = new AutoMocker();
            _handler = _mocker.CreateInstance<DefineMainAddressHandler>();
            _customerFixture = new CustomerFixture();
        }

        [Fact]
        public async Task DefineMainAddressShouldReturnValid()
        {
            //Arrange
            var customer = _customerFixture.CreateValidCustomerWithAddresses();
            var address = customer.Addresses
                .ElementAtOrDefault(_faker.Random.Int(0, customer.Addresses.Count - 1));

            var command = new DefineMainAddressCommand { CustomerId = Guid.NewGuid(), AddressId = address.Id };

            _mocker.GetMock<IUnitOfWork>()
                .Setup(c => c.CustomerRepository.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Include[]>()))
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

            var command = new DefineMainAddressCommand { CustomerId = Guid.NewGuid(), AddressId = address.Id };

            //Act
            var result = new DefineMainAddressValidator().Validate(command);

            //Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void DeleteAddressShouldReturnInvalid()
        {
            //Arrange
            var command = new DefineMainAddressCommand { CustomerId = Guid.Empty, AddressId = Guid.Empty };

            //Act
            var result = new DefineMainAddressValidator().Validate(command);

            //Assert
            Assert.False(result.IsValid);
            Assert.Equal(2, result.Errors.Count);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Equals("Informe o identificador do cliente"));
            Assert.Contains(result.Errors, e => e.ErrorMessage.Equals("Informe o identificador do endereço"));
        }

        [Fact]
        public async Task DefineMainAddressShouldThrowNotFoundCustomer()
        {
            //Arrange
            var command = new DefineMainAddressCommand { CustomerId = Guid.NewGuid(), AddressId = Guid.NewGuid() };

            _mocker.GetMock<IUnitOfWork>()
                .Setup(c => c.CustomerRepository.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Customer)null);

            //Act
            var result = await Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(command, CancellationToken.None));

            //Assert
            Assert.Equal("Cliente não encontrado", result.Message);
        }

        [Fact]
        public async Task DefineMainAddressShouldThrowDomainExceptionAddress()
        {
            //Arrange
            var customer = _customerFixture.CreateValidCustomerWithAddresses();
            var address = customer.Addresses
                .ElementAtOrDefault(_faker.Random.Int(0, customer.Addresses.Count - 1));

            var command = new DefineMainAddressCommand { CustomerId = Guid.NewGuid(), AddressId = Guid.NewGuid() };

            _mocker.GetMock<IUnitOfWork>()
                .Setup(c => c.CustomerRepository.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Include[]>()))
                .ReturnsAsync(customer);

            //Act
            var result = await Assert.ThrowsAsync<DomainException>(() =>
                _handler.Handle(command, CancellationToken.None));

            //Assert
            Assert.Equal(nameof(address), result.Message);
        }
    }
}
