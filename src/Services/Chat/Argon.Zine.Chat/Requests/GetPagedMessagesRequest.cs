namespace Argon.Zine.Chat.Requests;

public record struct GetPagedMessagesRequest(
    Guid UserId,
    int Limit,
    int Offset);