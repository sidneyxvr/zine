using Argon.Restaurants.Application.Commands;
using Argon.Restaurants.Application.Handlers;
using Argon.Restaurants.Domain;
using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using Moq.AutoMock;
using System.Threading;

namespace Argon.Zine.Restaurants.Tests.Application.Handlers;

public class OpenRestaurantHandlerTest
{
    private readonly Faker _faker;
    private readonly AutoMocker _mocker;
    private readonly OpenRestaurantHandler _handler;

    public OpenRestaurantHandlerTest()
    {
        _faker = new Faker("pt_BR");
        _mocker = new AutoMocker();
        _handler = _mocker.CreateInstance<OpenRestaurantHandler>();
    }

    [Fact]
    public async Task ShouldOpenRestaurant()
    {
        //Arrange
        var command = new OpenRestaurantCommand(Guid.NewGuid());
        MockRestaurantGetById(true);

        //Act
        var result = await _handler.Handle(command, default);
        var restaurant = (Restaurant)result.Result;

        //Assert
        Assert.True(restaurant.IsOpen);
        Assert.Single(restaurant.DomainEvents);
    }

    private void MockRestaurantGetById(bool isOpen = true)
    {
        var restaurant = new Restaurant(
            _faker.Company.CompanyName(),
            _faker.Company.CompanyName(),
            _faker.Company.Cnpj(),
            new User(
                Guid.NewGuid(),
                new Commom.DomainObjects.Name(
                    _faker.Person.FirstName,
                    _faker.Person.LastName),
            _faker.Person.Email), new Address(
                _faker.Address.StreetAddress(),
                _faker.Lorem.Letter(8),
                _faker.Lorem.Letter(20),
                _faker.Address.City(),
                "AA",
                "60000000",
                new Commom.DomainObjects.Location(_faker.Address.Latitude(), _faker.Address.Longitude())
                )
            );

        if (isOpen)
        {
            restaurant.Open();
        }

        _mocker.GetMock<IUnitOfWork>()
            .Setup(u => u.RestaurantRepository.GetByIdAsync(It.IsAny<Guid>(), Includes.None, It.IsAny<CancellationToken>()))
            .ReturnsAsync(restaurant);
    }
}