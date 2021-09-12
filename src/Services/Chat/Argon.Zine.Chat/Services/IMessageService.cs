using Argon.Zine.Chat.Models;
using Argon.Zine.Chat.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Argon.Zine.Chat.Services
{
    public interface IMessageService
    {
        Task<(Guid SenderId, Guid ReceiverId)> AddAsync(SendMessageRequest request);
        Task<IEnumerable<Message>> GetPagedMessagesAsync(GetPagedMessagesRequest request);
    }
}
