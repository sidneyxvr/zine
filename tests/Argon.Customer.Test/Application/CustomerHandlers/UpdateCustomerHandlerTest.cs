﻿using Argon.Core.DomainObjects;
using Argon.Customers.Application.CommandHandlers.CustomerHandlers;
using Argon.Customers.Application.Commands.CustomerCommands;
using Argon.Customers.Domain.AggregatesModel.CustomerAggregate;
using Argon.Customers.Test.Fixtures;
using Bogus;
using Moq;
using Moq.AutoMock;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Argon.Customers.Test.Application.CustomerHandlers
{
    public class UpdateCustomerHandlerTest
    {
        private readonly Faker _faker;
        private readonly AutoMocker _mocker;
        private readonly UpdateCustomerHandler _handler;
        private readonly CustomerFixture _customerFixture;

        public UpdateCustomerHandlerTest()
        {
            _faker = new Faker("pt_BR");
            _mocker = new AutoMocker();
            _handler = _mocker.CreateInstance<UpdateCustomerHandler>();
            _customerFixture = new CustomerFixture();
        }

        [Fact]
        public async Task UpdateCustomerShouldUpdate()
        {
            //Arrange
            var props = _customerFixture.GetCustomerTestDTO();
            var command = new UpdateCustomerCommand(Guid.NewGuid(), props.FirstName,
                props.Surname, props.Phone, props.BirthDate, props.Gender);

            _mocker.GetMock<ICustomerRepository>()
                .Setup(r => r.UnitOfWork.CommitAsync())
                .ReturnsAsync(true);

            _mocker.GetMock<ICustomerRepository>()
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(_customerFixture.CreateValidCustomerWithAddresses());

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task UpdateCustomerNullPropertiesShouldReturnInvalidWithErrorList()
        {
            //Arrange
            var command = new UpdateCustomerCommand(Guid.Empty, null, 
                null, null, DateTime.Now.AddYears(-19), Gender.Other);

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
            Assert.Equal(2, result.Errors.Count);
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe o nome"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe o sobrenome"));
        }

        [Fact]
        public async Task UpdateCustomerInvalidPropertiesShouldReturnInvalidWithErrorList()
        {
            //Arrange
            var command = new UpdateCustomerCommand(Guid.Empty, _faker.Random.String2(Name.MaxLengthFirstName + 1),
                _faker.Random.String2(Name.MaxLengthSurname + 1), _faker.Person.Email, DateTime.Now, 0);

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
            Assert.Equal(5, result.Errors.Count);
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals($"O nome de deve ter no máximo {Name.MaxLengthFirstName} caracteres"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals($"O sobrenome de deve ter no máximo {Name.MaxLengthSurname} caracteres"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Data de Nascimento inválida"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Número de celular inválido"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Sexo inválido"));
        }
    }
}
