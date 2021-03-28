using Argon.Core.DomainObjects;
using Bogus;
using Xunit;

namespace Argon.Core.Tests.DomainObjects
{
    public class NameTest
    {
        private readonly Faker _faker;

        public NameTest()
        {
            _faker = new Faker("pt_BR");
        }

        [Theory]
        [InlineData("  ")]
        [InlineData("")]
        [InlineData(null)]
        public void CreateNameEmptyFirstNameShouldThrowDomainException(string firstName)
        {
            //Arrange
            var surname = _faker.Person.LastName;

            //Act
            var result = Assert.Throws<DomainException>(() => new Name(firstName, surname));

            //Assert
            Assert.Equal("Informe o nome", result.Message);
        }

        [Fact]
        public void CreateNameMaxLengthFirstNameShouldThrowDomainException()
        {
            //Arrange
            var firstName = _faker.Lorem.Letter(_faker.Random.Int(51, 1000));
            var surname = _faker.Person.LastName;

            //Act
            var result = Assert.Throws<DomainException>(() => new Name(firstName, surname));

            //Assert
            Assert.Equal("O nome deve ter no máximo 50 caracteres", result.Message);
        }

        [Fact]
        public void CreateNameMaxLengthSurnameShouldThrowDomainException()
        {
            //Arrange
            var firstName = _faker.Person.FirstName;
            var surname = _faker.Lorem.Letter(_faker.Random.Int(51, 1000));

            //Act
            var result = Assert.Throws<DomainException>(() => new Name(firstName, surname));

            //Assert
            Assert.Equal("O sobrenome deve ter no máximo 50 caracteres", result.Message);
        }

        [Theory]
        [InlineData("  ")]
        [InlineData("")]
        [InlineData(null)]
        public void CreateNameEmptySurnameShouldThrowDomainException(string surname)
        {
            //Arrange
            var firstName = _faker.Person.FirstName;

            //Act
            var result = Assert.Throws<DomainException>(() => new Name(firstName, surname));

            //Assert
            Assert.Equal("Informe o sobrenome", result.Message);
        }

        [Fact]
        public void CreateNameValidNameShouldCreateSuccessfully()
        {
            //Arrange
            var firstName = _faker.Person.FirstName;
            var surname = _faker.Person.LastName;

            //Act
            var result = new Name(firstName, surname);

            //Assert
            Assert.Equal(result.FirstName, firstName);
            Assert.Equal(result.Surname, surname);
        }

        [Fact]
        public void GetFirstNameFromNameShouldWorkSuccessfully()
        {
            //Act
            var name = new Name("Nome", "da Pessoa Silva");

            //Assert
            Assert.Equal("Nome", name.FirstName);
        }


        [Fact]
        public void GetSurnameFromNameShouldWorkSuccessfully()
        {
            //Act
            var name = new Name("Nome", "da Pessoa Silva");

            //Assert
            Assert.Equal("da Pessoa Silva", name.Surname);
        }

        [Fact]
        public void GetFullNameFromNameShouldWorkSuccessfully()
        {
            //Act
            var name = new Name("Nome", "da Pessoa Silva");

            //Assert
            Assert.Equal("Nome da Pessoa Silva", name.FullName);
        }

        [Fact]
        public void CompreNameShouldReturnTrue()
        {
            //Act
            var name1 = new Name("User", "Teste");
            var name2 = new Name("User", "Teste");

            //Assert
            Assert.True(name1.Equals(name2));
            Assert.True(name1 == name2);
        }

        [Fact]
        public void CompreNameShouldReturnFalse()
        {
            //Act
            var name1 = new Name("User", "Teste1");
            var name2 = new Name("User", "Teste");

            //Assert
            Assert.False(name1.Equals(name2));
            Assert.True(name1 != name2);
        }
    }
}
