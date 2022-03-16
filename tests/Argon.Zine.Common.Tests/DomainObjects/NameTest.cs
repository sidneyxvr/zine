using Argon.Zine.Commom.DomainObjects;
using Bogus;
using Xunit;

namespace Argon.Zine.Commom.Tests.DomainObjects
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
            var LastName = _faker.Person.LastName;

            //Act
            var result = Assert.Throws<DomainException>(() => new Name(firstName, LastName));

            //Assert
            Assert.Equal(nameof(firstName), result.Message);
        }

        [Fact]
        public void CreateNameMaxLengthFirstNameShouldThrowDomainException()
        {
            //Arrange
            var firstName = _faker.Lorem.Letter(_faker.Random.Int(51, 1000));
            var LastName = _faker.Person.LastName;

            //Act
            var result = Assert.Throws<DomainException>(() => new Name(firstName, LastName));

            //Assert
            Assert.Equal(nameof(firstName), result.Message);
        }

        [Fact]
        public void CreateNameMaxLengthLastNameShouldThrowDomainException()
        {
            //Arrange
            var firstName = _faker.Person.FirstName;
            var lastName = _faker.Lorem.Letter(_faker.Random.Int(51, 1000));

            //Act
            var result = Assert.Throws<DomainException>(() => new Name(firstName, lastName));

            //Assert
            Assert.Equal(nameof(lastName), result.Message);
        }

        [Theory]
        [InlineData("  ")]
        [InlineData("")]
        [InlineData(null)]
        public void CreateNameEmptyLastNameShouldThrowDomainException(string lastName)
        {
            //Arrange
            var firstName = _faker.Person.FirstName;

            //Act
            var result = Assert.Throws<DomainException>(() => new Name(firstName, lastName));

            //Assert
            Assert.Equal(nameof(lastName), result.Message);
        }

        [Fact]
        public void CreateNameValidNameShouldCreateSuccessfully()
        {
            //Arrange
            var firstName = _faker.Person.FirstName;
            var LastName = _faker.Person.LastName;

            //Act
            var result = new Name(firstName, LastName);

            //Assert
            Assert.Equal(result.FirstName, firstName);
            Assert.Equal(result.LastName, LastName);
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
        public void GetLastNameFromNameShouldWorkSuccessfully()
        {
            //Act
            var name = new Name("Nome", "da Pessoa Silva");

            //Assert
            Assert.Equal("da Pessoa Silva", name.LastName);
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
