using Argon.Core.DomainObjects;
using Bogus;
using Xunit;

namespace Argon.Core.Tests.DomainObjects
{
    public class LocationTest
    {
        private readonly Faker _faker;

        public LocationTest()
        {
            _faker = new Faker("pt_BR");
        }

        [Fact]
        public void CreateLocationNullLatitudeShouldThrowDomainException()
        {
            //Arrange
            var longitude = _faker.Address.Longitude();

            //Act
            var result = Assert.Throws<DomainException>(() => new Location(null, longitude));

            //Assert
            Assert.Equal("Latitude ou Longitude inválida(s)", result.Message);
        }

        [Fact]
        public void CreateLocationNullLongitudeShouldThrowDomainException()
        {
            //Arrange
            var latitude = _faker.Address.Latitude();

            //Act
            var result = Assert.Throws<DomainException>(() => new Location(latitude, null));

            //Assert
            Assert.Equal("Latitude ou Longitude inválida(s)", result.Message);
        }

        [Theory]
        [InlineData(90.000000001)]
        [InlineData(91)]
        [InlineData(1e9)]
        [InlineData(-1e9)]
        [InlineData(-91)]
        [InlineData(-90.000000001)]
        public void CreateLocationLatitudeOutOfRangeShouldThrowDomainException(double latitude)
        {
            //Arrange
            var longitude = _faker.Address.Longitude();

            //Act
            var result = Assert.Throws<DomainException>(() => new Location(latitude, longitude));

            //Assert
            Assert.Equal("Latitude inválida", result.Message);
        }

        [Theory]
        [InlineData(180.000000001)]
        [InlineData(181)]
        [InlineData(1e9)]
        [InlineData(-1e9)]
        [InlineData(-181)]
        [InlineData(-180.000000001)]
        public void CreateLocationLongitudeOutOfRangeShouldThrowDomainException(double latitude)
        {
            //Arrange
            var longitude = _faker.Address.Longitude();

            //Act
            var result = Assert.Throws<DomainException>(() => new Location(latitude, longitude));

            //Assert
            Assert.Equal("Latitude inválida", result.Message);
        }

        [Fact]
        public void GetDistanceByLocationShouldWorkSuccessfully()
        {
            //Arrange
            var location1 = new Location(0, 20);
            var location2 = new Location(0, 30);

            //Act
            var distance = location1.GetDistance(location2);

            //Assert
            Assert.Equal(10, distance);
        }

        [Fact]
        public void GetDistanceByCoordinateShouldWorkSuccessfully()
        {
            //Arrange
            var location1 = new Location(0, 20);

            //Act
            var distance = location1.GetDistance(0, 30);

            //Assert
            Assert.Equal(10, distance);
        }

        [Fact]
        public void CompreLocationShouldReturnTrue()
        {
            //Act
            var location1 = new Location(10, 10);
            var location2 = new Location(10, 10);

            //Assert
            Assert.True(location1.Equals(location2));
            Assert.True(location1 == location2);
        }

        [Fact]
        public void CompreLocationShouldReturnFalse()
        {
            //Act
            var location1 = new Location(10, 11);
            var location2 = new Location(10, 10);

            //Assert
            Assert.False(location1.Equals(location2));
            Assert.True(location1 != location2);
        }
    }
}
