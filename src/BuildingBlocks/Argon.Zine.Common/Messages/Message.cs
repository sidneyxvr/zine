namespace Argon.Zine.Commom.Messages;

public record Message
{
    public string MessageType { get; protected set; }

    protected Message()
        => MessageType = GetType().Name;
}