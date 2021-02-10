using Argon.Core.DomainObjects;
using Argon.Customers.Application.CommandHandlers.AddressHandlers;
using Argon.Customers.Application.Commands.AddressCommands;
using Argon.Customers.Domain.AggregatesModel.CustomerAggregate;
using Argon.Customers.Test.Fixtures;
using Bogus;
using Moq;
using Moq.AutoMock;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Argon.Customers.Test.Application.AddressHandlers
{
    public class CreateAddressHandlerTest
    {
        private readonly Faker _faker;
        private readonly AutoMocker _mocker;
        private readonly CreateAddressHandler _handler;
        private readonly AddressFixture _addressFixture;
        private readonly CustomerFixture _customerFixture;

        public CreateAddressHandlerTest()
        {
            _faker = new Faker("pt_BR");
            _mocker = new AutoMocker();
            _handler = _mocker.CreateInstance<CreateAddressHandler>();
            _addressFixture = new AddressFixture();
            _customerFixture = new CustomerFixture();
        }

        [Fact]
        public async Task CreateAddressShouldCreate()
        {
            //Arrange
            var properties = _addressFixture.GetAddressTestDTO();

            var customer = _customerFixture.CreateValidCustomer();

            var command = new CreateAddressCommand(customer.Id, properties.Street, properties.Number, 
                properties.District, properties.City, properties.State, properties.Country, properties.PostalCode, 
                properties.Complement, properties.Latitude, properties.Longitude);

            _mocker.GetMock<ICustomerRepository>()
                .Setup(c => c.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(_customerFixture.CreateValidCustomerWithAddresses());

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
        public async Task CreateAddressShouldThrowNotFoundException()
        {
            //Arrange
            var properties = _addressFixture.GetAddressTestDTO();

            var customer = _customerFixture.CreateValidCustomer();

            var command = new CreateAddressCommand(customer.Id, properties.Street, properties.Number,
                properties.District, properties.City, properties.State, properties.Country, properties.PostalCode,
                properties.Complement, properties.Latitude, properties.Longitude);

            //Act
            var result = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));

            //Assert
            Assert.Equal("Cliente não encontrado", result.Message);
        }

        [Fact]
        public async Task CreateAddressShouldReturnInvalid()
        {
            //Arrange
            var command = new CreateAddressCommand(Guid.Empty, "", "", "", "", "", "", "", "", null, null);

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task CreateAddressNullFieldsShouldReturnInvalidWithErrorList()
        {
            //Arrange
            var command = new CreateAddressCommand(Guid.Empty, null, null, null, null, null, null, null, null, null, null);

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
            Assert.Equal(7, result.Errors.Count);
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe a cidade"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe o país"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe o bairro"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe a rua"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe o estado")); 
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe o CEP"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe o identificador do cliente"));
        }

        [Fact]
        public async Task CreateAddressEmptyFielsShouldReturnInvalidWithErrorList()
        {
            //Arrange
            var command = new CreateAddressCommand(Guid.NewGuid(), "",  _faker.Random.ULong(10_000_000_000).ToString(), 
                "", "", "", "", "", _faker.Lorem.Letter(_faker.Random.Int(51, 100)), null, null);

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
            Assert.Equal(8, result.Errors.Count);
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("A cidade deve ter entre 2 e 40 caracteres"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("O país deve ter entre 2 e 50 caracteres"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("O bairro deve ter entre 2 e 50 caracteres"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("A rua deve ter entre 2 e 50 caracteres"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Estado inválido"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("CEP inválido"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("O número deve ter no máximo 10 caracteres"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("O complemento deve ter no máximo 50 caracteres"));
        }

        [Fact]
        public async Task CreateAddressInvalidCoordinateShouldReturnInvalidWithErrorList()
        {
            //Arrange
            var properties = _addressFixture.GetAddressTestDTO();

            var command = new CreateAddressCommand(Guid.NewGuid(), properties.Street, properties.Number, properties.District, 
                properties.City, properties.State, properties.Country, properties.PostalCode, null, 91, 181);

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
            Assert.Equal(2, result.Errors.Count);
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Latitude inválida"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Longitude inválida"));
        }


        [Fact]
        public async Task CreateAddressLatitudeNullShouldReturnInvalid()
        {
            //Arrange
            var properties = _addressFixture.GetAddressTestDTO();

            var command = new CreateAddressCommand(Guid.NewGuid(), properties.Street, properties.Number, properties.District,
                properties.City, properties.State, properties.Country, properties.PostalCode, null, null, properties.Longitude);

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Latitude ou Longitude inválida(s)"));
        }
    }
}
