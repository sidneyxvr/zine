using Argon.Zine.Chat.Models;
using Argon.Zine.Chat.Requests;

namespace Argon.Zine.Chat.Repositories;

public interface IRoomRepository
{
    Task<Room> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Room>> GetPagedAsync(
        GetPagedRoomsRequest request, CancellationToken cancellationToken = default);
    Task AddAsync(Room room);
    Task UpdateAsync(Room room);
}