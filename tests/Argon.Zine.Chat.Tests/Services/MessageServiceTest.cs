using Argon.Zine.Chat.Models;
using Argon.Zine.Chat.Repositories;
using Argon.Zine.Chat.Requests;
using Argon.Zine.Chat.Services;
using Moq;
using Moq.AutoMock;
using System;
using System.Threading;

namespace Argon.Zine.Chat.Tests.Services;

public class MessageServiceTest
{
    private readonly AutoMocker _mocker;
    private readonly MessageService _messageService;

    public MessageServiceTest()
    {
        _mocker = new AutoMocker();
        _messageService = _mocker.CreateInstance<MessageService>();
    }

    [Fact]
    public async Task ShouldCreateMessage()
    {
        //Arrange
        var roomId = Guid.NewGuid();
        var restaurantUserId = Guid.NewGuid();
        var (senderId, senderName) = (Guid.NewGuid(), "sender");
        var request = new SendMessageRequest(roomId, "content");
        request.SetSender(senderId, senderName);
        MockGetRoomById(senderId, restaurantUserId);

        //Act
        var result = await _messageService.AddAsync(request);

        //Assert
        Assert.Equal(result.SenderId, senderId);
        Assert.Equal(result.ReceiverId, restaurantUserId);
    }

    private void MockGetRoomById(Guid customerUserId, Guid restaurantUserId)
        => _mocker.GetMock<IRoomRepository>()
        .Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(new Room("room", new User(customerUserId, "customer"), new User(restaurantUserId, "restaurant")));
}