using Argon.Zine.Chat.Models;

namespace Argon.Zine.Chat.Repositories;

public interface IMessageRepository
{
    Task AddAsync(Message message);
    Task UpdateAsync(Message message);
    Task<Message> GetByIdAsync(Guid id);
    Task<IEnumerable<Message>> GetPagedAsync(
        Guid userId, int limit, int offset,
        CancellationToken cancellationToken = default);
}