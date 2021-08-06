using Argon.Catalog.QueryStack.Queries;
using Argon.Chat.Requests;
using Argon.Chat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Argon.WebApp.API.Hubs
{
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

        [HubMethodName("createRoom")]
        public async Task CreateRoomAsync(CreateRoomRequest request)
        {
            var restaurant =
                (await _restaurantQueries.GetRestaurantDetailsByIdAsync(request.RestaurantId))!;

            var userId = Context.GetUserId();
            var userName = Context.GetUserFullName();

            var createRoom = new CreateRoomDTO
            {
                CustomerId = userId,
                CustomerName = userName,
                RestaurantId = restaurant.Id,
                RestaurantName = restaurant.Name,
                RestaurantLogoUrl = restaurant.LogoUrl,
            };

            await _roomService.AddAsync(createRoom);
        }

        [HubMethodName("sendMessage")]
        public async Task SendMessageAsync(SendMessageRequest request)
        {
            var (SenderId, ReceiverId) = await _messageService.AddAsync(request);

            var senderConnectionId = Context.ConnectionId;
            var receiverConnectionId =
                await _userService.GetConnectionIdByUserIdAsync(ReceiverId);

            await Clients.Clients(new[] { senderConnectionId, receiverConnectionId })
                .SendAsync("receiveMessage", new
                {
                    request.RoomId,
                    request.Content,
                    senderId = SenderId
                });
        }

        [HubMethodName("messages")]
        public async Task GetPagedMessageAsync(GetPagedMessagesRequest request)
        {
            request.SetUser(Context.GetUserId());
            var messages = await _messageService.GetPagedMessagesAsync(request);

            var connectionId = Context.ConnectionId;

            await Clients.Client(connectionId).SendAsync("receivePagedMessages", messages);
        }
    }

    public static class HubContextExtensions
    {
        public static Guid GetUserId(this HubCallerContext context)
            => new(context.User!.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);

        public static string GetUserFullName(this HubCallerContext context)
            => $"{context.User!.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.GivenName)!.Value} " +
            $"{context.User!.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.FamilyName)!.Value}";
    }

}
