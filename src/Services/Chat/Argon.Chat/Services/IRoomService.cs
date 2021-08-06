using Argon.Chat.Requests;
using System.Threading.Tasks;

namespace Argon.Chat.Services
{
    public interface IRoomService
    {
        Task AddAsync(CreateRoomDTO createRoom);
    }
}
