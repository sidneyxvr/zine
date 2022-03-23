using Argon.Zine.Catalog.QueryStack.Queries;
using Argon.Zine.Chat.Models;
using Argon.Zine.Chat.Requests;
using Argon.Zine.Chat.Services;
using Argon.Zine.Commom.DomainObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Argon.Zine.App.Api.Hubs;

[Authorize]
public class ChatHub : Hub
{
    private readonly IUserService _userService;
    private readonly IRoomService _roomService;
    private readonly IMessageService _messageService;
    private readonly IRestaurantQueries _restaurantQueries;

    public ChatHub(
        IUserService userService,
        IRoomService roomService,
        IMessageService messageService,
        IRestaurantQueries restaurantQueries)
    {
        _userService = userService;
        _roomService = roomService;
        _messageService = messageService;
        _restaurantQueries = restaurantQueries;
    }

    [HubMethodName("create-room")]
    public async Task CreateRoomAsync(CreateRoomRequest request)
    {
        var restaurant =
            (await _restaurantQueries.GetRestaurantDetailsByIdAsync(request.RestaurantId))!;

        var userId = Context.GetUserId();
        var userName = Context.GetUserFullName();

        var createRoom = new CreateRoomDto(userId, userName,
            restaurant.Id, restaurant.Name, restaurant.LogoUrl);

        await _roomService.AddAsync(createRoom);
    }

    [HubMethodName("send-message")]
    public async Task SendMessageAsync(SendMessageRequest request)
    {
        request.SetSender(Context.GetUserId(), Context.GetUserFullName());

        var (senderId, receiverId) = await _messageService.AddAsync(request);

        var senderConnectionId = Context.ConnectionId;
        var receiverConnectionId =
            await _userService.GetConnectionIdByUserIdAsync(receiverId);

        await Clients.Clients(new[] { senderConnectionId, receiverConnectionId })
            .SendAsync("receive-message", new
            {
                request.RoomId,
                request.Content,
                senderId
            });
    }

    [HubMethodName("messages")]
    public async Task<IEnumerable<Message>> GetPagedMessageAsync(GetPagedMessagesRequest request)
        => await _messageService.GetPagedMessagesAsync(request);

    [HubMethodName("rooms")]
    public async Task<IEnumerable<Room>> GetPagedRoomAsync(GetPagedRoomsRequest request)
        => await _roomService.GetPagedMessagesAsync(request);
}

public static class HubContextExtensions
{
    public static Guid GetUserId(this HubCallerContext context)
        => context.User is { } user
            ? new(user.FindFirstValue(ClaimTypes.NameIdentifier))
            : throw new NullReferenceException(nameof(user));

    public static string GetUserFullName(this HubCallerContext context)
        => context.User is { } user
            ? $"{user.FindFirstValue(ClaimTypes.GivenName)} {user.FindFirstValue(ClaimTypes.Surname)}"
            : throw new NullReferenceException(nameof(user));
}