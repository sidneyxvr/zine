using Argon.Zine.Commom.Data.EventSourcing;
using Argon.Zine.Commom.Messages;
using EventStore.ClientAPI;
using System.Text;
using System.Text.Json;

namespace Argon.Zine.EventSourcing;

public class EventSourcingStorage : IEventSourcingStorage
{
    protected bool ConnectionClosed;
    private readonly IEventStoreConnection _connection;
    private static readonly JsonSerializerOptions options = new() { WriteIndented = true };
    public EventSourcingStorage(IEventStoreConnection connection)
        => (_connection, ConnectionClosed) = (connection, true);

    private async Task ConnectAsync()
    {
        await _connection.ConnectAsync();
        ConnectionClosed = false;
    }

    public async Task AddAsync<TEvent>(TEvent @event) where TEvent : Event
    {
        if (ConnectionClosed) await ConnectAsync();

        var formated = FormatEvent(@event);

        await _connection.AppendToStreamAsync(
            @event.AggregateId.ToString(),
            ExpectedVersion.Any,
            formated);
    }

    public async Task<IEnumerable<StoredEvent>> GetEventsByAggregateIdAsync(Guid aggregateId)
    {
        if (ConnectionClosed) await ConnectAsync();

        var stream = await _connection.ReadStreamEventsBackwardAsync(aggregateId.ToString(), 0, 500, false);

        return stream.Events.Select(MapEvent);
    }

    private static IEnumerable<EventData> FormatEvent<TEvent>(TEvent data) where TEvent : Event
    {
        var jsonData = JsonSerializer.Serialize<object>(data, options);

        yield return new EventData(
            Guid.NewGuid(),
            data.MessageType,
            true,
            Encoding.UTF8.GetBytes(jsonData),
            null);
    }

    private static StoredEvent MapEvent(ResolvedEvent @event)
    {
        var dataEncoded = Encoding.UTF8.GetString(@event.Event.Data);
        var timestamp = JsonSerializer.Deserialize<JsonElement>(dataEncoded)
            .GetProperty("Timestamp")
            .GetDateTime();

        return new StoredEvent(@event.Event.EventId, @event.Event.EventType, timestamp, dataEncoded);
    }
}