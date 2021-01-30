using Argon.Core.DomainObjects;
using Xunit;

namespace Argon.Core.Test.DomainObjects
{
    public class PhoneTest
    {
        [Theory]
        [InlineData("(83) 99694-3980")]
        [InlineData("+5583996943980")]
        [InlineData("083996943980")]
        [InlineData("33449999")]
        [InlineData("  ")]
        [InlineData("")]
        public void CreatePhoneInvalidNumberShouldThrowDomainException(string phone)
        {
            //Act
            var result = Assert.Throws<DomainException>(() => new Phone(phone));

            //Assert
            Assert.Equal("Número de celular inválido", result.Message);
        }

        [Theory]
        [InlineData("56930056970")]
        [InlineData("89979125502")]
        [InlineData("23956816486")]
        [InlineData("74968192294")]
        public void CreatePhoneValidNumberShouldCreateSuccessfully(string phone)
        {
            //Act
            var result = new Phone(phone);

            //Assert
            Assert.Equal(result.Number, phone);
        }
    }
}
