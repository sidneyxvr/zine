namespace Argon.Zine.Chat.Requests;

public record SendMessageRequest(Guid RoomId, string Content)
{
    public Guid SenderId { get; private set; }
    public string? SenderName { get; private set; }

    public void SetSender(Guid senderId, string senderName)
        => (SenderId, SenderName) = (senderId, senderName);
}

public record SendMessageDto
{
    public Guid RoomId { get; init; }
    public Guid SenderId { get; init; }
    public string SenderName { get; init; } = null!;
    public string Content { get; init; } = null!;
}