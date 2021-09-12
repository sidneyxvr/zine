using Argon.Zine.Chat.Requests;
using System.Threading.Tasks;

namespace Argon.Zine.Chat.Services
{
    public interface IRoomService
    {
        Task AddAsync(CreateRoomDTO createRoom);
    }
}
