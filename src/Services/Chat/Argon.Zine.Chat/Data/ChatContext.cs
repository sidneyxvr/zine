using Argon.Zine.Chat.Models;
using MongoDB.Driver;

namespace Argon.Zine.Chat.Data;

public class ChatContext
{
    public IMongoCollection<Room> Rooms { get; }
    public IMongoCollection<Message> Messages { get; }

    public ChatContext(IMongoDatabase mongoDatabase)
    {
        Rooms = mongoDatabase.GetCollection<Room>("Rooms");
        Messages = mongoDatabase.GetCollection<Message>("Messages");
    }
}