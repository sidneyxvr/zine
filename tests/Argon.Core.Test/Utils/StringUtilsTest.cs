using Argon.Core.Utils;
using Xunit;

namespace Argon.Core.Test.Utils
{
    public class StringUtilsTest
    {
        [Fact]
        public void GetOnlyNumberShoudWorkSuccessfully()
        {
            //Arrange
            var stringWithNumber = "a1^2W3¨4B5!6-7/8@9";

            //Act
            var stringWithoutNumber = stringWithNumber.OnlyNumbers();

            //Assert
            Assert.Equal("123456789", stringWithoutNumber);
        }
    }
}
