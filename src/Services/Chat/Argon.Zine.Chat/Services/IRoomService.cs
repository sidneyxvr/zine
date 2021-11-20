using Argon.Zine.Chat.Requests;

namespace Argon.Zine.Chat.Services;

public interface IRoomService
{
    Task AddAsync(CreateRoomDto createRoom);
}