using Argon.Core.DomainObjects;
using Argon.Core.Messages.IntegrationCommands;
using Argon.Core.Messages.IntegrationCommands.Validators;
using Argon.Customers.Application;
using Argon.Customers.Domain;
using Argon.Customers.Tests.Fixtures;
using Bogus;
using Moq;
using Moq.AutoMock;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Argon.Customers.Tests.Application.CustomerHandlers
{
    public class CreateCustomerHandlerTest
    {
        private readonly Faker _faker;
        private readonly AutoMocker _mocker;
        private readonly CreateCustomerHandler _handler;
        private readonly CustomerFixture _customerFixture;

        public CreateCustomerHandlerTest()
        {
            _faker = new Faker("pt_BR");
            _mocker = new AutoMocker();
            _handler = _mocker.CreateInstance<CreateCustomerHandler>();
            _customerFixture = new CustomerFixture();
        }

        [Fact]
        public async Task CreateCustomerShouldCreate()
        {
            //Arrange
            var props = _customerFixture.GetCustomerTestDTO();
            var command = new CreateCustomerCommand
            {
                UserId = Guid.NewGuid(),
                FirstName = props.FirstName,
                LastName = props.LastName,
                Email = props.Email,
                Phone = props.Phone,
                Cpf = props.Cpf,
                BirthDate = props.BirthDate,
                Gender = props.Gender
            };

            _mocker.GetMock<IUnitOfWork>()
                .Setup(u => u.CustomerRepository.AddAsync(It.IsAny<Customer>()));

            _mocker.GetMock<IUnitOfWork>()
                .Setup(u => u.CommitAsync())
                .ReturnsAsync(true);

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void CreateCustomerNullPropertiesShouldReturnInvalidWithErrorList()
        {
            //Arrange
            var command = new CreateCustomerCommand
            {
                UserId = Guid.Empty,
                BirthDate = DateTime.Now.AddYears(-19),
                Gender = Gender.Other
            };

            //Act
            var result = new CreateCustomerValidator().Validate(command);

            //Assert
            Assert.False(result.IsValid);
            Assert.Equal(4, result.Errors.Count);
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe o nome"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe o sobrenome"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe o CPF"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Informe o email"));
        }

        [Fact]
        public void CreateCustomerShouldReturnInvalidWithErrorList()
        {
            //Arrange
            var command = new CreateCustomerCommand
            {
                UserId = Guid.Empty,
                FirstName = _faker.Random.String2(Name.MaxLengthFirstName + 1),
                LastName = _faker.Random.String2(Name.MaxLengthLastName + 1),
                Email = "a@b",
                Phone = "999",
                Cpf = "12345678900",
                BirthDate = DateTime.Now,
                Gender = 0
            };

            //Act
            var result = new CreateCustomerValidator().Validate(command);

            //Assert
            Assert.False(result.IsValid);
            Assert.Equal(7, result.Errors.Count);
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals($"O nome deve ter no máximo {Name.MaxLengthFirstName} caracteres"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals($"O sobrenome deve ter no máximo {Name.MaxLengthLastName} caracteres"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("CPF inválido"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals($"O email deve ter entre {Email.MinLength} e {Email.MaxLength} caracteres"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Número de celular inválido"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Data de Nascimento inválida"));
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Sexo inválido"));
        }

        [Fact]
        public void CreateCustomerShouldReturnInvalidEmail()
        {
            //Arrange
            var props = _customerFixture.GetCustomerTestDTO();
            var command = new CreateCustomerCommand
            {
                UserId = Guid.NewGuid(),
                FirstName = props.FirstName,
                LastName = props.LastName,
                Email = _faker.Person.FullName,
                Cpf = props.Cpf,
                BirthDate = props.BirthDate,
                Gender = props.Gender
            };

            //Act
            var result = new CreateCustomerValidator().Validate(command);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, a => a.ErrorMessage.Equals("Email inválido"));
        }
    }
}
