using Argon.Zine.Basket.Requests;
using Argon.Zine.Basket.Services;
using Moq.AutoMock;

namespace Argon.Zine.Basket.Tests.Services;

public class BasketServiceTest
{
    private readonly AutoMocker _mocker;
    private readonly BasketService _basketService;

    public BasketServiceTest()
    {
        _mocker = new AutoMocker();
        _basketService = _mocker.CreateInstance<BasketService>();
    }

    [Fact]
    public async Task ShouldAddProductToBasket()
    {
        //Arrange
        var request = new ProductToBasketDto(Guid.NewGuid(), "product", 
            20.9m, 3, null, Guid.NewGuid(), "restaurant", null);

        //Act
        var result = await _basketService.AddProductToBasketAsync(request);

        //Assert
        //Assert.Equal(62.7m, result.Total);
        Assert.Single(result.Products);
    }
}