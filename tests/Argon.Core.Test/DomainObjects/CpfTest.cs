using Argon.Core.DomainObjects;
using Bogus;
using Bogus.Extensions.Brazil;
using Xunit;

namespace Argon.Core.Test.DomainObjects
{
    public class CpfTest
    {
        private readonly Faker _faker;

        public CpfTest()
        {
            _faker = new Faker("pt_BR");
        }

        [Theory]
        [InlineData("00000000000")]
        [InlineData("12345678910")]
        [InlineData("teste@email.com")]
        [InlineData("ana maria")]
        [InlineData("54965421000186")]
        [InlineData("65055394")]
        [InlineData("262994501")]
        public void CreateCPFInvalidNumberShouldThrowDomainException(string cpf)
        {
            //Act
            var result = Assert.Throws<DomainException>(() => new Cpf(cpf));

            //Assert
            Assert.Equal("CPF inválido", result.Message);
        }

        [Theory]
        [InlineData("   ")]
        [InlineData("")]
        [InlineData(null)]
        public void CreateCPFEmptyNumberShouldThrowDomainException(string cpf)
        {
            //Act
            var result = Assert.Throws<DomainException>(() => new Cpf(cpf));

            //Assert
            Assert.Equal("Informe o CPF", result.Message);
        }

        [Fact]
        public void CreateCpfValidNumberShouldCreateSuccessfully()
        {
            //Arrange
            var cpf = _faker.Person.Cpf(false);

            //Act
            var result = new Cpf(cpf);

            //Assert
            Assert.Equal(result.Number, cpf);
        }
    }
}
