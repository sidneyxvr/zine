using Argon.Core.DomainObjects;
using Argon.Customers.Application.CommandHandlers.AddressHandlers;
using Argon.Customers.Application.Commands.AddressCommands;
using Argon.Customers.Domain.AggregatesModel.CustomerAggregate;
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
    public class UpdateAddressHandlerTest
    {
        private readonly Faker _faker;
        private readonly AutoMocker _mocker;
        private readonly UpdateAddressHandler _handler;
        private readonly AddressFixture _addressFixture;
        private readonly CustomerFixture _customerFixture;

        public UpdateAddressHandlerTest()
        {
            _faker = new Faker("pt_BR");
            _mocker = new AutoMocker();
            _handler = _mocker.CreateInstance<UpdateAddressHandler>();
            _addressFixture = new AddressFixture();
            _customerFixture = new CustomerFixture();
        }

        [Fact]
        public async Task UpdateAddressShouldUpdate()
        {
            //Arrange
            var properties = _addressFixture.GetAddressTestDTO();

            var customer = _customerFixture.CreateValidCustomerWithAddresses();
            var address = customer.Addresses
                .ElementAtOrDefault(_faker.Random.Int(0, customer.Addresses.Count - 1));

            var command = new UpdateAddressCommand(customer.Id, address.Id, properties.Street, properties.Number,
                properties.District, properties.City, properties.State, properties.Country, properties.PostalCode,
                properties.Complement, properties.Latitude, properties.Longitude);

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
        public async Task UpdateAddressShouldThrowNotFoundException()
        {
            //Arrange
            var properties = _addressFixture.GetAddressTestDTO();

            var customer = _customerFixture.CreateValidCustomerWithAddresses();

            var command = new UpdateAddressCommand(customer.Id, properties.Id, properties.Street, properties.Number,
                properties.District, properties.City, properties.State, properties.Country, properties.PostalCode,
                properties.Complement, properties.Latitude, properties.Longitude);

            _mocker.GetMock<ICustomerRepository>()
                .Setup(c => c.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(customer);

            //Act
            var result = await Assert.ThrowsAsync<NotFoundException>(() => 
                _handler.Handle(command, CancellationToken.None));

            //Assert
            Assert.Equal("Endereço não encontrado", result.Message);
        }

        [Fact]
        public async Task UpdateAddressShouldReturnInvalid()
        {
            //Arrange
            var command = new UpdateAddressCommand(Guid.Empty, Guid.Empty, "", "", "", "", "", "", "", "", null, null);

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task UpdateAddressNullFieldsShouldReturnInvalidWithErrorList()
        {
            //Arrange
            var command = new UpdateAddressCommand(Guid.Empty, Guid.Empty, null, null, null, null, null, null, null, null, null, null);

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
            Assert.Equal(8, result.Errors.Count);
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe a cidade"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe o país"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe o bairro"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe a rua"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe o estado"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe o CEP"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe o identificador do endereço"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe o identificador do cliente"));
        }

        [Fact]
        public async Task UpdateAddressEmptyFielsShouldReturnInvalidWithErrorList()
        {
            //Arrange
            var command = new UpdateAddressCommand(Guid.NewGuid(), Guid.NewGuid(), "", 
                _faker.Random.ULong(10_000_000_000).ToString(), "", "", "", "", "", 
                _faker.Lorem.Letter(_faker.Random.Int(51, 100)), null, null);

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
        public async Task UpdateAddressInvalidCoordinateShouldReturnInvalidWithErrorList()
        {
            //Arrange
            var properties = _addressFixture.GetAddressTestDTO();

            var command = new UpdateAddressCommand(Guid.NewGuid(), Guid.NewGuid(), properties.Street, properties.Number, properties.District,
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
        public async Task UpdateAddressLatitudeNullShouldReturnInvalid()
        {
            //Arrange
            var properties = _addressFixture.GetAddressTestDTO();

            var command = new UpdateAddressCommand(Guid.NewGuid(), properties.Id, properties.Street, properties.Number, properties.District,
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
