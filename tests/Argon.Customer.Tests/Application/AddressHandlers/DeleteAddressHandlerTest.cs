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
        public async Task DeleteAddressShouldThrowNotFoundCustomer()
        {
            //Arrange
            var command = new DeleteAddressCommand { CustomerId = Guid.NewGuid(), AddressId = Guid.NewGuid() };

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
        public async Task DeleteAddressShouldThrowDomainExceptionAddress()
        {
            //Arrange
            var customer = _customerFixture.CreateValidCustomerWithAddresses();
            var address = customer.Addresses
                .ElementAtOrDefault(_faker.Random.Int(0, customer.Addresses.Count - 1));

            var command = new DeleteAddressCommand { CustomerId = Guid.NewGuid(), AddressId = Guid.NewGuid() };

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
