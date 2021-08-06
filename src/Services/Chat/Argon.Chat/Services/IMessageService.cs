using Argon.Chat.Models;
using Argon.Chat.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Argon.Chat.Services
{
    public interface IMessageService
    {
        Task<(Guid SenderId, Guid ReceiverId)> AddAsync(SendMessageRequest request);
        Task<IEnumerable<Message>> GetPagedMessagesAsync(GetPagedMessagesRequest request);
    }
}
