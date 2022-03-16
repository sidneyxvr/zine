using Argon.Zine.Commom.Messages;

namespace Argon.Zine.Ordering.Application.Commands;

public record SubmitOrderCommand : Command
{
    public Guid CustomerId { get; init; }
    public Guid PaymentMethodId { get; init; }
    public Guid RestaurantId { get; init; }
    public string Street { get; init; } = null!;
    public string? Number { get; init; }
    public string District { get; init; } = null!;
    public string City { get; init; } = null!;
    public string State { get; init; } = null!;
    public string Country { get; init; } = null!;
    public string PostalCode { get; init; } = null!;
    public string? Complement { get; init; }
    public IEnumerable<OrderItemDTO> OrderItems { get; init; } = null!;
}

public record OrderItemDTO(Guid ProductId, string ProductName, 
    string? ProductImageUrl, decimal UnitPrice, int Units);