using System.Text.Json.Serialization;

namespace Argon.Zine.Commom.Messages;

public record Message
{
    [JsonIgnore]
    public string MessageType { get; protected set; }

    protected Message()
        => MessageType = GetType().Name;
}