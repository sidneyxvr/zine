using Argon.Core.DomainObjects;
using Argon.Core.Internationalization;
using Xunit;

namespace Argon.Core.Test.DomainObjects
{
    public class PhoneTest
    {
        private readonly Localizer _localizer;

        public PhoneTest()
        {
            _localizer = Localizer.GetLocalizer();
        }

        [Theory]
        [InlineData("(83) 99694-3980")]
        [InlineData("+5583996943980")]
        [InlineData("083996943980")]
        [InlineData("33449999")]
        public void CreateCPFInvalidNumberShouldThrowDomainException(string phone)
        {
            //Act
            var result = Assert.Throws<DomainException>(() => new Phone(phone));

            //Assert
            Assert.Equal(result.Message, _localizer.GetTranslation("InvalidPhone"));
        }

        [Theory]
        [InlineData("56930056970")]
        [InlineData("89979125502")]
        [InlineData("23956816486")]
        [InlineData("74968192294")]
        public void CreateCPFValidNumberShouldCreateSuccessfully(string phone)
        {
            //Act
            var result = new Phone(phone);

            //Assert
            Assert.Equal(result.Number, phone);
        }
    }
}
