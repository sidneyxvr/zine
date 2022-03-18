using Argon.Zine.Commom.Messages;

namespace Argon.Zine.Ordering.Application.Commands;

public record SubmitOrderCommand(Guid CustomerId, Guid PaymentMethodId, 
    Guid RestaurantId, AddressDto Address, IEnumerable<OrderItemDto> OrderItems) : Command;

public record AddressDto(string Street, string? Number, string District, string City, 
    string State, string Country, string PostalCode, string? Complement);

public record OrderItemDto(Guid ProductId, string ProductName, 
    string? ProductImageUrl, decimal UnitPrice, int Units);