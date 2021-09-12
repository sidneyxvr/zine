using Argon.Zine.Chat.Data;
using Argon.Zine.Chat.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Argon.Zine.Chat.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMongoCollection<Message> _messages;

        public MessageRepository(IOptions<ChatDatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            _messages = database.GetCollection<Message>("Messages");
        }

        public async Task AddAsync(Message message)
            => await _messages.InsertOneAsync(message);

        public async Task<Message> GetByIdAsync(Guid id)
            => await _messages.Find(Builders<Message>.Filter.Eq(r => r.Id, id))
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<Message>> GetPagedAsync(Guid userId, int limit, int offset)
            => await _messages.Find(Builders<Message>.Filter.Eq(m => m.Sender.Id, userId))
            .Skip(offset)
            .Limit(limit)
            .ToListAsync();

        public async Task UpdateAsync(Message message)
            => await _messages.ReplaceOneAsync(r => r.Id == message.Id, message);
    }
}
