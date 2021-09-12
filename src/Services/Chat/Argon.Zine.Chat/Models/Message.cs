using Argon.Zine.Core.DomainObjects;
using System;

namespace Argon.Zine.Chat.Models
{
    public class Message : Entity
    {
        public Guid RoomId { get; private set; }
        public User Sender { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? SeenAt { get; private set; }

        public Message(Guid roomId, User sender, string content, DateTime createdAt)
        {
            RoomId = roomId;
            Sender = sender;
            Content = content;
            CreatedAt = createdAt;
        }
    }
}
