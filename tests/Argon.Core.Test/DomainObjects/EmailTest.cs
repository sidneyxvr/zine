using Argon.Core.DomainObjects;
using Bogus;
using Xunit;

namespace Argon.Core.Test.DomainObjects
{
    public class EmailTest
    {
        private readonly Faker _faker;

        public EmailTest()
        {
            _faker = new Faker("pt_BR");
        }

        [Theory]
        [InlineData("plainaddress")]
        [InlineData("#@%^%#$@#$@#.com")]
        [InlineData("@example.com")]
        [InlineData("Joe Smith <email@example.com>")]
        [InlineData("email.example.com")]
        [InlineData("email@example@example.com")]
        [InlineData(".email@example.com")]
        [InlineData("email.@example.com")]
        [InlineData("email..email@example.com")]
        [InlineData("あいうえお@example.com")]
        [InlineData("email@example.com (Joe Smith)")]
        [InlineData("email@example")]
        [InlineData("email@-example.com")]
        [InlineData("email@111.222.333.44444")]
        [InlineData("email@example..com")]
        [InlineData("Abc..123@example.com")]
        public void CreateCPFInvalidAddressShouldThrowDomainException(string email)
        {
            //Act
            var result = Assert.Throws<DomainException>(() => new Email(email));

            //Assert
            Assert.Equal("Email inválido", result.Message);
        }


        [Theory]
        [InlineData("  ")]
        [InlineData("")]
        [InlineData(null)]
        public void CreateCPFEmptyAddressShouldThrowDomainException(string email)
        {
            //Act
            var result = Assert.Throws<DomainException>(() => new Email(email));

            //Assert
            Assert.Equal("Informe o email", result.Message);
        }

        [Fact]
        public void CreateEmailValidAddressShouldCreateSuccessfully()
        {
            //Arrange
            var email = _faker.Person.Email;
            
            //Act
            var result = new Email(email);

            //Assert
            Assert.Equal(result.Address, email);
        }

        [Fact]
        public void CreateEmailImplictValidAddressShouldCreateSuccessfully()
        {
            //Arrange
            var emailAddress = _faker.Person.Email;

            //Act
            Email email = emailAddress;

            //Assert
            Assert.Equal(emailAddress, email.Address);
        }

        [Fact]
        public void CompreEmailShouldReturnTrue()
        {
            //Act
            var email1 = new Email("teste@email.com");
            var email2 = new Email("teste@email.com");

            //Assert
            Assert.True(email1.Equals(email2));
            Assert.True(email1 == email2);
        }

        [Fact]
        public void CompreEmailShouldReturnFalse()
        {
            //Act
            var email1 = new Email("teste@email.com");
            var email2 = new Email("teste1@email.com");

            //Assert
            Assert.False(email1.Equals(email2));
            Assert.True(email1 != email2);
        }
    }
}
