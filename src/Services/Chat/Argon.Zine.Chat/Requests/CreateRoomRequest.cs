namespace Argon.Zine.Chat.Requests;

public record CreateRoomRequest(Guid RestaurantId);

public record CreateRoomDto(
    Guid CustomerId,
    string CustomerName,
    Guid RestaurantId,
    string RestaurantName,
    string? RestaurantLogoUrl);