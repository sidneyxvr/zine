using Argon.Zine.Chat.Data;
using Argon.Zine.Chat.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Argon.Zine.Chat.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly ChatContext _context;

    public MessageRepository(ChatContext context)
        => _context = context;

    public async Task AddAsync(Message message)
        => await _context.Messages.InsertOneAsync(message);

    public async Task<Message> GetByIdAsync(Guid id)
        => await _context.Messages.AsQueryable()
            .FirstOrDefaultAsync(m => m.Id == id);

    public async Task<IEnumerable<Message>> GetPagedAsync(
        Guid userId, int limit, int offset, CancellationToken cancellationToken = default)
        => await _context.Messages.AsQueryable()
            .Where(m => m.Sender.Id == userId)
            .Skip(offset)
            .Take(limit)
            .ToListAsync(cancellationToken);

    public async Task UpdateAsync(Message message)
        => await _context.Messages.ReplaceOneAsync(r => r.Id == message.Id, message);
}