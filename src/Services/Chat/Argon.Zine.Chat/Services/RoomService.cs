using Argon.Zine.Chat.Models;
using Argon.Zine.Chat.Repositories;
using Argon.Zine.Chat.Requests;
using System.Threading.Tasks;

namespace Argon.Zine.Chat.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
            => _roomRepository = roomRepository;

        public async Task AddAsync(CreateRoomDTO createRoom)
        {
            var customerUser = new User(createRoom.CustomerId, createRoom.CustomerName);
            var restaurantUser = new User(createRoom.RestaurantId, 
                createRoom.RestaurantName, createRoom.RestaurantLogoUrl);

            var room = new Room($"{createRoom.OrderSequentialId}", customerUser, restaurantUser);

            await _roomRepository.AddAsync(room);
        }
    }
}
