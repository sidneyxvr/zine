using Argon.Identity.Tests.Fixture;
using Argon.Identity.Tests.Fixtures;
using Argon.Zine.Core.Communication;
using Argon.Zine.Core.DomainObjects;
using Argon.Zine.Core.Messages.IntegrationCommands;
using Argon.Zine.Core.Utils;
using Argon.Zine.Identity.Models;
using Argon.Zine.Identity.Requests;
using Argon.Zine.Identity.Services;
using Bogus;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Argon.Identity.Tests.Services
{
    [Collection(nameof(IdentityTestsFixtureCollection))]
    public class AccountServiceTest
    {
        private readonly Faker _faker;
        private readonly AutoMocker _mocker;
        private readonly AccountService _accountService;
        private readonly CustomerUserFixture _customerUserFixture;

        public AccountServiceTest(IdentityTestsFixture identityFixture)
        {
            _customerUserFixture = new();
            _faker = identityFixture.Faker;
            _mocker = identityFixture.Mocker;
            _accountService = _mocker.CreateInstance<AccountService>();
        }

        [Fact]
        public async Task CreateCustomerUserShouldCreate()
        {
            //Arrange
            var customerUser = _customerUserFixture.CreateCustomerUser();

            var request = new CustomerUserRequest
            {
                FirstName = customerUser.FirstName,
                LastName = customerUser.LastName,
                Email = customerUser.Email,
                Cpf = customerUser.Cpf,
                BirthDate = customerUser.BirthDate,
                Gender = customerUser.Gender,
                Password = customerUser.Password
            };

            _mocker.GetMock<UserManager<User>>()
                .Setup(u => u.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _mocker.GetMock<UserManager<User>>()
                .Setup(u => u.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _mocker.GetMock<UserManager<User>>()
                .Setup(u => u.GenerateEmailConfirmationTokenAsync(It.IsAny<User>()))
                .ReturnsAsync(Guid.NewGuid().ToString());

            _mocker.GetMock<IBus>()
                .Setup(b => b.SendAsync(It.IsAny<CreateCustomerCommand>()))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            _mocker.GetMock<IEmailService>()
                .Setup(e => e.SendEmailConfirmationAccountAsync(It.IsAny<string>(), It.IsAny<string>()));

            //Act
            var result = await _accountService.CreateCustomerUserAsync(request);

            //Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task CreateCustomerUserNullFieldsRequestShouldReturnsInvalid()
        {
            //Arrange
            var request = new CustomerUserRequest
            {
                FirstName = null,
                LastName = null,
                Email = null,
                Cpf = null,
                BirthDate = DateTime.MinValue,
                Gender = 0,
                Password = null
            };

            //Act
            var result = await _accountService.CreateCustomerUserAsync(request);

            //Assert
            Assert.Equal(7, result.Errors.Count);
            Assert.Contains(result.Errors, r => r.ErrorMessage.Equals("Sexo inválido"));
            Assert.Contains(result.Errors, r => r.ErrorMessage.Equals("Data de nascimento inválida"));
            Assert.Contains(result.Errors, r => r.ErrorMessage.Equals("Informe o CPF"));
            Assert.Contains(result.Errors, r => r.ErrorMessage.Equals("Informe o email"));
            Assert.Contains(result.Errors, r => r.ErrorMessage.Equals("Informe o nome"));
            Assert.Contains(result.Errors, r => r.ErrorMessage.Equals("Informe o sobrenome"));
            Assert.Contains(result.Errors, r => r.ErrorMessage.Equals("Informe a senha"));
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task CreateCustomerUserMaxLegthFieldsRequestShouldReturnsInvalid()
        {
            //Arrange
            var request = new CustomerUserRequest
            {
                FirstName = _faker.Lorem.Letter(Name.MaxLengthFirstName + 1),
                LastName = _faker.Lorem.Letter(Name.MaxLengthLastName + 1),
                Email = _faker.Lorem.Letter(Email.MaxLength + 1),
                Cpf = _faker.Random.String(CpfValidator.NumberLength + 1, '0', '9'),
                BirthDate = DateTime.MaxValue,
                Phone = _faker.Random.String(Phone.NumberMaxLength + 1, '0', '9'),
                Gender = Gender.Other,
                Password = _faker.Lorem.Letter(101)
            };

            //Act
            var result = await _accountService.CreateCustomerUserAsync(request);

            //Assert
            Assert.False(result.IsValid);
            Assert.Equal(7, result.Errors.Count);
            Assert.Contains(result.Errors, r => r.ErrorMessage.Equals($"O nome deve ter no máximo {Name.MaxLengthFirstName} caracteres"));
            Assert.Contains(result.Errors, r => r.ErrorMessage.Equals($"O sobrenome deve ter no máximo {Name.MaxLengthLastName} caracteres"));
            Assert.Contains(result.Errors, r => r.ErrorMessage.Equals("CPF inválido"));
            Assert.Contains(result.Errors, r => r.ErrorMessage.Equals($"O email deve ter entre {Email.MinLength} e {Email.MaxLength} caracteres"));
            Assert.Contains(result.Errors, r => r.ErrorMessage.Equals("Data de nascimento inválida"));
            Assert.Contains(result.Errors, r => r.ErrorMessage.Equals("Número de celular inválido"));
            Assert.Contains(result.Errors, r => r.ErrorMessage.Equals("A senha deve ter entre 8 e 100 caracteres"));
        }

        [Fact]
        public async Task CreateCustomerUserMinLegthFieldsRequestShouldReturnsInvalid()
        {
            //Arrange
            var customerUser = _customerUserFixture.CreateCustomerUser();

            var request = new CustomerUserRequest
            {
                FirstName = customerUser.FirstName,
                LastName = customerUser.LastName,
                Email = "a@b",
                Cpf = customerUser.Cpf,
                BirthDate = customerUser.BirthDate,
                Phone = _faker.Random.String(Phone.NumberMinLength - 1, '0', '9'),
                Gender = customerUser.Gender,
                Password = _faker.Lorem.Letter(7)
            };

            //Act
            var result = await _accountService.CreateCustomerUserAsync(request);

            //Assert
            Assert.Equal(3, result.Errors.Count);
            Assert.Contains(result.Errors, r => r.ErrorMessage.Equals("O email deve ter entre 5 e 254 caracteres"));
            Assert.Contains(result.Errors, r => r.ErrorMessage.Equals("Número de celular inválido"));
            Assert.Contains(result.Errors, r => r.ErrorMessage.Equals("A senha deve ter entre 8 e 100 caracteres"));
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task CreateCustomerUserManagerShouldReturnsInvalid()
        {
            //Arrange
            var customerUser = _customerUserFixture.CreateCustomerUser();

            var request = new CustomerUserRequest
            {
                FirstName = customerUser.FirstName,
                LastName = customerUser.LastName,
                Email = customerUser.Email,
                Cpf = customerUser.Cpf,
                BirthDate = customerUser.BirthDate,
                Gender = customerUser.Gender,
                Password = customerUser.Password
            };

            _mocker.GetMock<UserManager<User>>()
                .Setup(u => u.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Code = "any", Description = "Some error" }));

            //Act
            var result = await _accountService.CreateCustomerUserAsync(request);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task CreateCustomerBusShouldReturnsInvalid()
        {
            //Arrange
            var customerUser = _customerUserFixture.CreateCustomerUser();

            var request = new CustomerUserRequest
            {
                FirstName = customerUser.FirstName,
                LastName = customerUser.LastName,
                Email = customerUser.Email,
                Cpf = customerUser.Cpf,
                BirthDate = customerUser.BirthDate,
                Gender = customerUser.Gender,
                Password = customerUser.Password
            };

            _mocker.GetMock<UserManager<User>>()
                .Setup(u => u.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _mocker.GetMock<IBus>()
                .Setup(b => b.SendAsync(It.IsAny<CreateCustomerCommand>()))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult(
                    new List<ValidationFailure>
                    {
                        new ValidationFailure("any", "Some error")
                    }));

            //Act
            var result = await _accountService.CreateCustomerUserAsync(request);

            //Assert
            Assert.False(result.IsValid);
            _mocker.GetMock<UserManager<User>>().Verify(u => u.DeleteAsync(It.IsAny<User>()), Times.Once);
        }


        [Fact]
        public async Task ConfirmEmailAccountIvalidEmailShouldReturnInvalid()
        {
            //Arrange
            var customerUser = _customerUserFixture.CreateCustomerUser();

            var request = new EmailAccountConfirmationRequest
            {
                Email = "a@b",
                Token = _faker.Lorem.Letter(50)
            };

            //Act
            var result = await _accountService.ConfirmEmailAccountAsync(request);

            //Assert
            Assert.False(result.IsValid);
            _mocker.GetMock<UserManager<User>>().Verify(u => u.FindByEmailAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task ConfirmEmailAccountEmptyEmailShouldReturnInvalid()
        {
            //Arrange
            var customerUser = _customerUserFixture.CreateCustomerUser();

            var request = new EmailAccountConfirmationRequest
            {
                Token = _faker.Lorem.Letter(50)
            };

            //Act
            var result = await _accountService.ConfirmEmailAccountAsync(request);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, error => error.ErrorMessage.Equals("Informe o email"));
        }

        [Fact]
        public async Task ConfirmEmailAccountUserNotFoundShouldReturnInvalid()
        {
            //Arrange
            var customerUser = _customerUserFixture.CreateCustomerUser();

            var request = new EmailAccountConfirmationRequest
            {
                Email = customerUser.Email,
                Token = _faker.Lorem.Letter(50)
            };

            //Act
            var result = await _accountService.ConfirmEmailAccountAsync(request);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Equals("Não foi possível confirmar seu email"));
            _mocker.GetMock<UserManager<User>>().Verify(u => u.FindByEmailAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ConfirmEmailAccountEmailNotConfirmedShouldReturnInvalid()
        {
            //Arrange
            var customerUser = _customerUserFixture.CreateCustomerUser();

            var request = new EmailAccountConfirmationRequest
            {
                Email = customerUser.Email,
                Token = _faker.Lorem.Letter(50)
            };

            _mocker.GetMock<UserManager<User>>().Invocations.Clear();

            _mocker.GetMock<UserManager<User>>()
                .Setup(u => u.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Code = "any", Description = "Some error" }));

            _mocker.GetMock<UserManager<User>>()
                .Setup(u => u.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new User());

            //Act
            var result = await _accountService.ConfirmEmailAccountAsync(request);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Equals("Não foi possível confirmar seu email"));
            _mocker.GetMock<UserManager<User>>().Verify(u => u.FindByEmailAsync(It.IsAny<string>()), Times.Once);
            _mocker.GetMock<UserManager<User>>().Verify(u => u.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ConfirmEmailAccountShouldReturnValid()
        {
            //Arrange
            var customerUser = _customerUserFixture.CreateCustomerUser();

            var request = new EmailAccountConfirmationRequest
            {
                Email = customerUser.Email,
                Token = _faker.Lorem.Letter(50)
            };

            _mocker.GetMock<UserManager<User>>().Invocations.Clear();

            _mocker.GetMock<UserManager<User>>()
                .Setup(u => u.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _mocker.GetMock<UserManager<User>>()
                .Setup(u => u.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new User());

            //Act
            var result = await _accountService.ConfirmEmailAccountAsync(request);

            //Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<UserManager<User>>().Verify(u => u.FindByEmailAsync(It.IsAny<string>()), Times.Once);
            _mocker.GetMock<UserManager<User>>().Verify(u => u.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        //private void MockStringLocalizer(AutoMocker mocker)
        //{
        //    mocker.Use<IStringLocalizerFactory>(new StringLocalizerFactory());
            
        //    mocker.Use<IStringLocalizer<AccountService>>(
        //        LocalizerHelper.CreateInstanceStringLocalizer<AccountService>());
        //}
    }
}
