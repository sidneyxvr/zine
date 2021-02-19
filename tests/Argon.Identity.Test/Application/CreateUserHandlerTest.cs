using Argon.Core.Communication;
using Argon.Core.DomainObjects;
using Argon.Core.Messages.IntegrationCommands;
using Argon.Identity.Application.CommandHandlers;
using Argon.Identity.Application.Commands;
using Bogus;
using Bogus.Extensions.Brazil;
using Microsoft.AspNetCore.Identity;
using Moq;
using Moq.AutoMock;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Argon.Identity.Test.Application
{
    public class CreateUserHandlerTest
    {
        private readonly Faker _faker;
        private readonly AutoMocker _mocker;
        private readonly CreateUserHandler _handler;

        public CreateUserHandlerTest()
        {
            _mocker = new AutoMocker();
            _faker = new Faker("pt_BR");
            _handler = _mocker.CreateInstance<CreateUserHandler>();
        }

        [Fact]
        public async Task CreateUserShouldCreate()
        {
            //Arrange
            var person = _faker.Person;
            var command = new CreateUserCommand(person.FirstName, person.LastName, 
                person.Email, "88999887766", person.Cpf(), DateTime.Now.AddYears(-19), 
                _faker.PickRandom<Gender>(), _faker.Internet.Password());

            _mocker.GetMock<UserManager<IdentityUser<Guid>>>()
                .Setup(r => r.CreateAsync(It.IsAny<IdentityUser<Guid>>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _mocker.GetMock<IBus>()
                .Setup(r => r.SendAsync(It.IsAny<CreateCustomerCommand>()))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }
    }
}
