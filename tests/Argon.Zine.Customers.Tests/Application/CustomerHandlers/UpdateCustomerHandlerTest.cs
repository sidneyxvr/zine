﻿using Argon.Zine.Customers.Application.Commands;
using Argon.Zine.Customers.Application.Handlers;
using Argon.Zine.Customers.Application.Validators;
using Argon.Zine.Customers.Domain;
using Argon.Zine.Customers.Tests.Fixtures;
using Argon.Zine.Core.DomainObjects;
using Bogus;
using Microsoft.Extensions.Localization;
using Moq;
using Moq.AutoMock;
using Xunit;
using System.Threading.Tasks;
using System;
using System.Threading;

namespace Argon.Zine.Customers.Tests.Application.CustomerHandlers
{
    public class UpdateCustomerHandlerTest
    {
        private readonly Faker _faker;
        private readonly AutoMocker _mocker;
        private readonly UpdateCustomerHandler _handler;
        private readonly CustomerFixture _customerFixture;
        private readonly IStringLocalizer<UpdateCustomerValidator> _localizer;

        public UpdateCustomerHandlerTest()
        {
            _faker = new Faker("pt_BR");
            _mocker = new AutoMocker();
            _handler = _mocker.CreateInstance<UpdateCustomerHandler>();
            _customerFixture = new CustomerFixture();
            _localizer = LocalizerHelper.CreateInstanceStringLocalizer<UpdateCustomerValidator>();
        }

        [Fact]
        public async Task UpdateCustomerShouldUpdate()
        {
            //Arrange
            var props = _customerFixture.GetCustomerTestDTO();
            var command = new UpdateCustomerCommand
            {
                FirstName = props.FirstName,
                LastName = props.LastName,
                Phone = props.Phone,
                BirthDate = props.BirthDate,
                Gender = props.Gender
            };

            _mocker.GetMock<IUnitOfWork>()
                .Setup(u => u.CommitAsync())
                .ReturnsAsync(true);

            _mocker.GetMock<IUnitOfWork>()
                .Setup(r => r.CustomerRepository
                    .GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Include>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_customerFixture.CreateValidCustomerWithAddresses());

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<IUnitOfWork>()
                .Verify(r => r.CustomerRepository
                    .UpdateAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Once);
            _mocker.GetMock<IUnitOfWork>().Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public void UpdateCustomerNullPropertiesShouldReturnInvalidWithErrorList()
        {
            //Arrange
            var command = new UpdateCustomerCommand
            {
                BirthDate = DateTime.Now.AddYears(-19),
                Gender = Gender.Other
            };

            //Act
            var result = new UpdateCustomerValidator(_localizer).Validate(command);

            //Assert
            Assert.False(result.IsValid);
            Assert.Equal(2, result.Errors.Count);
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe o nome"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe o sobrenome"));
        }

        [Fact]
        public void UpdateCustomerInvalidPropertiesShouldReturnInvalidWithErrorList()
        {
            //Arrange
            var command = new UpdateCustomerCommand
            {
                FirstName = _faker.Random.String2(Name.MaxLengthFirstName + 1),
                LastName = _faker.Random.String2(Name.MaxLengthLastName + 1),
                Phone = _faker.Person.Email,
                BirthDate = DateTime.Now,
                Gender = 0
            };

            //Act
            var result = new UpdateCustomerValidator(_localizer).Validate(command);

            //Assert
            Assert.False(result.IsValid);
            Assert.Equal(5, result.Errors.Count);
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals($"O nome deve ter no máximo {Name.MaxLengthFirstName} caracteres"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals($"O sobrenome deve ter no máximo {Name.MaxLengthLastName} caracteres"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Data de Nascimento inválida"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Número de celular inválido"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Sexo inválido"));
        }
    }
}