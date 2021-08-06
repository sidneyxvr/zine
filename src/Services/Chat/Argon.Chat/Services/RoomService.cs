using Argon.Chat.Models;
using Argon.Chat.Repositories;
using Argon.Chat.Requests;
using System.Threading.Tasks;

namespace Argon.Chat.Services
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
