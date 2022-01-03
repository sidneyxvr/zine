using Argon.Zine.Chat.Data;
using Argon.Zine.Chat.Models;
using MongoDB.Driver;

namespace Argon.Zine.Chat.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly ChatContext _context;

    public MessageRepository(ChatContext context)
        => _context = context;


    public async Task AddAsync(Message message)
        => await _context.Messages.InsertOneAsync(message);

    public async Task<Message> GetByIdAsync(Guid id)
        => await _context.Messages.Find(Builders<Message>.Filter.Eq(r => r.Id, id))
        .FirstOrDefaultAsync();

    public async Task<IEnumerable<Message>> GetPagedAsync(
        Guid userId, int limit, int offset, CancellationToken cancellationToken = default)
        => await _context.Messages.Find(Builders<Message>.Filter.Eq(m => m.Sender.Id, userId))
        .Skip(offset)
        .Limit(limit)
        .ToListAsync(cancellationToken);

    public async Task UpdateAsync(Message message)
        => await _context.Messages.ReplaceOneAsync(r => r.Id == message.Id, message);
}