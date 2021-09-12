using Argon.Zine.Chat.Models;
using System;
using System.Threading.Tasks;

namespace Argon.Zine.Chat.Repositories
{
    public interface IRoomRepository
    {
        Task<Room> GetByIdAsync(Guid id);
        Task AddAsync(Room room);
        Task UpdateAsync(Room room);
    }
}
