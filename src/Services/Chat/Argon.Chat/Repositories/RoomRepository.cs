using Argon.Chat.Data;
using Argon.Chat.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Argon.Chat.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly IMongoCollection<Room> _rooms;

        public RoomRepository(IOptions<ChatDatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            _rooms = database.GetCollection<Room>("Rooms");
        }

        public async Task AddAsync(Room room)
            => await _rooms.InsertOneAsync(room);

        public async Task<Room> GetByIdAsync(Guid id)
            => await _rooms.Find(Builders<Room>.Filter.Eq(r => r.Id, id))
            .FirstOrDefaultAsync();

        public async Task UpdateAsync(Room room)
            => await _rooms.ReplaceOneAsync(r => r.Id == room.Id, room);
    }
}
