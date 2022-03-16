using Argon.Zine.Chat.Models;
using Argon.Zine.Chat.Requests;

namespace Argon.Zine.Chat.Services;

public interface IMessageService
{
    Task<(Guid SenderId, Guid ReceiverId)> AddAsync(SendMessageRequest request);
    Task<IEnumerable<Message>> GetPagedMessagesAsync(
        GetPagedMessagesRequest request, CancellationToken cancellationToken = default);
}