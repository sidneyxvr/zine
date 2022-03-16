namespace Argon.Zine.Chat.Requests;

public record GetPagedMessagesRequest(Guid RoomId, int Limit, int Offset);