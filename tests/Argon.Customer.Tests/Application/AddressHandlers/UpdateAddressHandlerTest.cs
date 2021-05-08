﻿using Argon.Core.DomainObjects;
using Argon.Customers.Application.CommandHandlers;
using Argon.Customers.Application.Commands;
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

            var command = new UpdateAddressCommand
            {
                CustomerId = customer.Id,
                AddressId = address.Id,
                Street = properties.Street,
                Number = properties.Number,
                District = properties.District,
                City = properties.City,
                State = properties.State,
                PostalCode = properties.PostalCode,
                Complement = properties.Complement,
                Latitude = properties.Latitude,
                Longitude = properties.Longitude
            };

            _mocker.GetMock<IUnitOfWork>()
                .Setup(c => c.CustomerRepository.GetAddressAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(address);

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
        public async Task UpdateAddressShouldReturnInvalid()
        {
            //Arrange
            var command = new UpdateAddressCommand
            {
                CustomerId = Guid.Empty,
                AddressId = Guid.Empty,
                Street = "",
                Number = "",
                District = "",
                City = "",
                State = "",
                PostalCode = "",
                Complement = ""
            };

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task UpdateAddressNullFieldsShouldReturnInvalidWithErrorList()
        {
            //Arrange
            var command = new UpdateAddressCommand
            {
                CustomerId = Guid.Empty,
                AddressId = Guid.Empty
            };

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
            Assert.Equal(7, result.Errors.Count);
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe a cidade"));
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
            var command = new UpdateAddressCommand
            {
                CustomerId = Guid.NewGuid(),
                AddressId = Guid.NewGuid(),
                Street = "",
                Number = _faker.Random.ULong(10_000_000_000).ToString(),
                District = "",
                City = "",
                State = "",
                PostalCode = "",
                Complement = _faker.Lorem.Letter(_faker.Random.Int(51, 100))
            };

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
            Assert.Equal(7, result.Errors.Count);
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("A cidade deve ter entre 2 e 40 caracteres"));
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

            var command = new UpdateAddressCommand
            {
                AddressId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                Street = properties.Street,
                Number = properties.Number,
                District = properties.District,
                City = properties.City,
                State = properties.State,
                PostalCode = properties.PostalCode,
                Complement = null,
                Latitude = 91,
                Longitude = 181,
            };

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

            var command = new UpdateAddressCommand
            {
                AddressId = properties.Id,
                CustomerId = Guid.NewGuid(),
                Street = properties.Street,
                Number = properties.Number,
                District = properties.District,
                City = properties.City,
                State = properties.State,
                PostalCode = properties.PostalCode,
                Longitude = properties.Longitude,
            };

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Latitude ou Longitude inválida(s)"));
        }
    }
}
