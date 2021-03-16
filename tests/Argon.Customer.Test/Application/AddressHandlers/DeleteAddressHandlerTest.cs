using Argon.Core.DomainObjects;
using Argon.Customers.Application.CommandHandlers.AddressHandlers;
using Argon.Customers.Application.Commands.AddressCommands;
using Argon.Customers.Domain;
using Argon.Customers.Test.Fixtures;
using Bogus;
using Moq;
using Moq.AutoMock;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Argon.Customers.Test.Application.AddressHandlers
{
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

            var command = new DeleteAddressCommand { CustomerId = Guid.NewGuid(), AddressId = address.Id };

            _mocker.GetMock<ICustomerRepository>()
                .Setup(c => c.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(customer);

            _mocker.GetMock<ICustomerRepository>()
                .Setup(r => r.UnitOfWork.CommitAsync())
                .ReturnsAsync(true);

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<ICustomerRepository>().Verify(c => c.UnitOfWork.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAddressShouldReturnInvalid()
        {
            //Arrange
            var command = new DeleteAddressCommand { CustomerId = Guid.Empty, AddressId = Guid.Empty };

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
            Assert.Equal(2, result.Errors.Count);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Equals("Informe o identificador do cliente"));
            Assert.Contains(result.Errors, e => e.ErrorMessage.Equals("Informe o identificador do endereço"));
        }

        [Fact]
        public async Task DeleteAddressShouldThrowNotFoundCustomer()
        {
            //Arrange
            var command = new DeleteAddressCommand { CustomerId = Guid.NewGuid(), AddressId = Guid.NewGuid() };

            //Act
            var result = await Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(command, CancellationToken.None));

            //Assert
            Assert.Equal("Cliente não encontrado", result.Message);
        }

        [Fact]
        public async Task DeleteAddressShouldThrowNotFoundExceptionAddress()
        {
            //Arrange
            var customer = _customerFixture.CreateValidCustomerWithAddresses();
            var address = customer.Addresses
                .ElementAtOrDefault(_faker.Random.Int(0, customer.Addresses.Count - 1));

            var command = new DeleteAddressCommand { CustomerId = Guid.NewGuid(), AddressId = Guid.NewGuid() };

            _mocker.GetMock<ICustomerRepository>()
                .Setup(c => c.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(customer);

            //Act
            var result = await Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(command, CancellationToken.None));

            //Assert
            Assert.Equal("Endereço não encontrado", result.Message);
        }
    }
}
