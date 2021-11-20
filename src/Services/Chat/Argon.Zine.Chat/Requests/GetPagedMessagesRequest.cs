namespace Argon.Zine.Chat.Requests;

public class GetPagedMessagesRequest
{
    public Guid UserId { get; private set; }
    public int Limit { get; init; }
    public int Offset { get; init; }

    public void SetUser(Guid userId)
        => UserId = userId;
}