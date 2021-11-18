using Argon.Zine.Catalog.QueryStack.Queries;
using Argon.Zine.Chat.Models;
using Argon.Zine.Chat.Requests;
using Argon.Zine.Chat.Services;
using Microsoft.AspNetCore.SignalR;
using System.IdentityModel.Tokens.Jwt;

namespace Argon.Zine.App.Api.Hubs
{
    //[Authorize]
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

            var createRoom = new CreateRoomDto
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
        public async Task<IEnumerable<Message>> GetPagedMessageAsync(GetPagedMessagesRequest request)
        {
            request.SetUser(Context.GetUserId());
            return await _messageService.GetPagedMessagesAsync(request);
        }
    }

    public static class HubContextExtensions
    {
        public static Guid GetUserId(this HubCallerContext context)
            => new ("A18B8113-2392-4263-1B78-08D94A24E18B");//new(context.User!.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);

        public static string GetUserFullName(this HubCallerContext context)
            => $"{context.User!.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.GivenName)!.Value} " +
            $"{context.User!.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.FamilyName)!.Value}";
    }
}
