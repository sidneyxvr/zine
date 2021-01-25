using Argon.Core.DomainObjects;
using Argon.Core.Internationalization;
using Bogus;
using Xunit;

namespace Argon.Core.Test.DomainObjects
{
    public class FullNameTest
    {
        private readonly Faker _faker;
        private readonly Localizer _localizer;

        public FullNameTest()
        {
            _faker = new Faker("pt_BR");
            _localizer = Localizer.GetLocalizer();
        }

        [Theory]
        [InlineData("Maria")]
        [InlineData("João")]
        [InlineData("anamaria")]
        [InlineData("ana maria")]
        [InlineData("ana Maria")]
        [InlineData("ana-Maria")]
        [InlineData("Ana-Maria")]
        public void CreateFullNameInvalidNameShouldThrowDomainException(string fullName)
        {
            //Act
            var result = Assert.Throws<DomainException>(() => new FullName(fullName));

            //Assert
            Assert.Equal(result.Message, _localizer.GetTranslation("InvalidFullName"));
        }

        [Fact]
        public void CreateFullNameValidNameShouldCreateSuccessfully()
        {
            //Arrange
            var fullName = _faker.Person.FullName;

            //Act
            var result = new FullName(fullName);

            //Assert
            Assert.Equal(result.Name, fullName);
        }

        [Fact]
        public void GetFirstNameFromFullNameShouldWorkSuccessfully()
        {
            //Arrange
            var fullName = new FullName("Nome da Pessoa Silva");

            //Act
            var firstName = fullName.FirstName();

            //Assert
            Assert.Equal("Nome", firstName);
        }
    }
}
