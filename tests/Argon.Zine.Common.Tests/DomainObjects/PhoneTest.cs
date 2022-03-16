using Argon.Zine.Commom.DomainObjects;
using Xunit;

namespace Argon.Zine.Commom.Tests.DomainObjects
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
            Assert.Equal(nameof(Phone), result.Message);
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

        [Fact]
        public void CreatePhoneImplictValidNumberShouldCreateSuccessfully()
        {
            //Act
            Phone phone = "56930056970";

            //Assert
            Assert.Equal("56930056970", phone.Number);
        }

        [Fact]
        public void ComprePhoneNumberShouldReturnTrue()
        {
            //Act
            Phone phone1 = "56930056970";
            var phone2 = new Phone("56930056970");

            //Assert
            Assert.True(phone1.Equals(phone2));
            Assert.True(phone1 == phone2);
        }

        [Fact]
        public void ComprePhoneNumberShouldReturnFalse()
        {
            //Act
            Phone phone1 = "56930056970";
            var phone2 = new Phone("56930056971");

            //Assert
            Assert.False(phone1.Equals(phone2));
            Assert.True(phone1 != phone2);
        }

        [Fact]
        public void ComprePhoneNumberBothNullShouldReturnTrue()
        {
            //Act
            Phone phone1 = null;
            Phone phone2 = null;

            //Assert
            Assert.True(phone1 == phone2);
        }

        [Fact]
        public void ComprePhoneNumberLeftNullShouldReturnFalse()
        {
            //Act
            Phone phone1 = null;
            Phone phone2 = "56930056971";

            //Assert
            Assert.False(phone1 == phone2);
        }

        [Fact]
        public void GetHashCodeShouldReturnHashCode()
        {
            //Act
            Phone phone = "56930056971";

            //Assert
            Assert.NotEqual(0, phone.GetHashCode());
        }
    }
}
