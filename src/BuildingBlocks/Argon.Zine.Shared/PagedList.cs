using System.Text.Json.Serialization;

namespace Argon.Zine.Shared;

public record PagedList<T>
{
    public IEnumerable<T> List { get; init; }
    public int Count { get; init; }

    [JsonConstructor]
    public PagedList(IEnumerable<T> list, int count)
        => (List, Count) = (list, count);
}