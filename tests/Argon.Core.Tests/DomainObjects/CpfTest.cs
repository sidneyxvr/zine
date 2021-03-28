using Argon.Core.DomainObjects;
using Bogus;
using Bogus.Extensions.Brazil;
using RTools_NTS.Util;
using Xunit;

namespace Argon.Core.Tests.DomainObjects
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

        [Theory]
        [InlineData("54917489008")]
        [InlineData("38546466076")]
        [InlineData("71980843031")]
        [InlineData("68326140040")]
        [InlineData("13830803800")]
        public void CreateCpfValidNumberShouldCreateSuccessfully(string cpf)
        {
            //Act
            var result = new Cpf(cpf);

            //Assert
            Assert.Equal(result.Number, cpf);
        }

        [Fact]
        public void CreateCpfImplictValidNumberShouldCreateSuccessfully()
        {
            //Arrange
            var cpfNumber = _faker.Person.Cpf(false);

            //Act
            Cpf cpf = cpfNumber;

            //Assert
            Assert.Equal(cpf.Number, cpfNumber);
        }

        [Fact]
        public void CompreCpfShouldReturnTrue()
        {
            //Act
            var cpf1 = new Cpf("54917489008");
            Cpf cpf2 = "54917489008";

            //Assert
            Assert.True(cpf1.Equals(cpf2));
            Assert.True(cpf1 == cpf2);
        }

        [Fact]
        public void CompreCpfShouldReturnFalse()
        {
            //Act
            var cpf1 = new Cpf("54917489008");
            Cpf cpf2 = "38546466076";

            //Assert
            Assert.False(cpf1.Equals(cpf2));
            Assert.True(cpf1 != cpf2);
        }
    }
}
