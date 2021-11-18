namespace Argon.Zine.Core.Messages;

public class StoredEvent
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public DateTime OperationDate { get; set; }
    public string Data { get; set; }

    public StoredEvent(Guid id, string type, DateTime operationDate, string data)
    {
        Id = id;
        Type = type;
        OperationDate = operationDate;
        Data = data;
    }
}