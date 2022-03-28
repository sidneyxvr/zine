using Argon.Zine.Chat.Models;
using Argon.Zine.Chat.Repositories;
using Argon.Zine.Chat.Requests;

namespace Argon.Zine.Chat.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;

    public RoomService(IRoomRepository roomRepository)
        => _roomRepository = roomRepository;

    public async Task AddAsync(CreateRoomDto createRoom)
    {
        var customerUser = new User(createRoom.CustomerId, createRoom.CustomerName);
        var restaurantUser = new User(createRoom.RestaurantId,
            createRoom.RestaurantName, createRoom.RestaurantLogoUrl);

        var room = new Room($"{createRoom.CustomerId}", customerUser, restaurantUser);

        await _roomRepository.AddAsync(room);
    }

    public async Task<IEnumerable<Room>> GetPagedMessagesAsync(GetPagedRoomsRequest request)
        => await _roomRepository.GetPagedAsync(request);
}