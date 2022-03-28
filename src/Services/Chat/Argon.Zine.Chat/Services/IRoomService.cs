using Argon.Zine.Chat.Models;
using Argon.Zine.Chat.Requests;

namespace Argon.Zine.Chat.Services;

public interface IRoomService
{
    Task AddAsync(CreateRoomDto createRoom);
    Task<IEnumerable<Room>> GetPagedMessagesAsync(GetPagedRoomsRequest request);
}