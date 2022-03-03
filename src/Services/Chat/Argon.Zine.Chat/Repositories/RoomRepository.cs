using Argon.Zine.Chat.Data;
using Argon.Zine.Chat.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Argon.Zine.Chat.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly ChatContext _context;

    public RoomRepository(ChatContext context)
        => _context = context;

    public async Task AddAsync(Room room)
        => await _context.Rooms.InsertOneAsync(room);

    public async Task<Room> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.Rooms.AsQueryable()
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

    public async Task UpdateAsync(Room room)
        => await _context.Rooms.ReplaceOneAsync(r => r.Id == room.Id, room);
}