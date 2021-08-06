using Argon.Chat.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Argon.Chat.Repositories
{
    public interface IMessageRepository
    {
        Task AddAsync(Message message);
        Task UpdateAsync(Message message);
        Task<Message> GetByIdAsync(Guid id);
        Task<IEnumerable<Message>> GetPagedAsync(Guid userId, int limit, int offset);
    }
}
