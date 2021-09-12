using System;

namespace Argon.Zine.Chat.Requests
{
    public class SendMessageRequest
    {
        public Guid RoomId { get; init; } 
        public string Content { get; init; } = null!;
        public Guid SenderId { get; private set; }
        public string? SenderName { get; private set; }

        public void SetSender(Guid senderId, string senderName)
            => (SenderId, SenderName) = (senderId, SenderName);
    }

    public class SendMessageDTO
    {
        public Guid RoomId { get; init; }
        public Guid SenderId { get; init; }
        public string SenderName { get; init; } = null!;
        public string Content { get; init; } = null!;
    }
}
