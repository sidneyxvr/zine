using Argon.Zine.Chat.Models;
using Argon.Zine.Chat.Requests;

namespace Argon.Zine.Chat.Repositories;

public interface IMessageRepository
{
    Task AddAsync(Message message);
    Task UpdateAsync(Message message);
    Task<Message> GetByIdAsync(Guid id);
    Task<IEnumerable<Message>> GetPagedAsync(
        GetPagedMessagesRequest request, CancellationToken cancellationToken = default);
}