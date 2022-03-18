using Argon.Zine.Ordering.Application.Commands;
using Argon.Zine.Ordering.Application.Handlers;
using Argon.Zine.Ordering.Domain;
using Moq;
using Moq.AutoMock;
using System.Threading;

namespace Argon.Zine.Ordering.Tests.Application;

public class SubmitOrderHandlerTest
{
    private readonly AutoMocker _mocker;
    private readonly SubmitOrderHandler _handler;

    public SubmitOrderHandlerTest()
    {
        _mocker = new AutoMocker();
        _handler = _mocker.CreateInstance<SubmitOrderHandler>();
    }

    [Fact]
    public async Task ShouldSubmitOrder()
    {
        //Arrange
        var addressDto = new AddressDto("street", "123", "district", "city", "state", "country", "00000000", null);
        var orderItems = new[] {
            new OrderItemDto(Guid.NewGuid(), "produtct 1", null, 10.9m, 2),
            new OrderItemDto(Guid.NewGuid(), "produtct 1", null, 12.3m, 3)
        };
        var command = new SubmitOrderCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), addressDto, orderItems);
        MockOrderRepository(); 

        //Act
        var result = await _handler.Handle(command, default);
        var order = (Order)result.Result!;

        //Assert
        Assert.Equal(2, order.OrderItems.Count);
        Assert.Equal(58.7m, order.Total);
    }

    private void MockOrderRepository()
        => _mocker.GetMock<IUnitOfWork>()
        .Setup(u => u.OrderRepository.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()));
}