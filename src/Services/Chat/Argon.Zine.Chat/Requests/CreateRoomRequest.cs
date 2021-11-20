namespace Argon.Zine.Chat.Requests;

public record CreateRoomRequest
{
    public Guid RestaurantId { get; init; }
}

public record CreateRoomDto
{
    public int OrderSequentialId { get; init; }
    public Guid CustomerId { get; init; }
    public string CustomerName { get; init; } = null!;
    public Guid RestaurantId { get; init; }
    public string RestaurantName { get; init; } = null!;
    public string? RestaurantLogoUrl { get; init; }
}