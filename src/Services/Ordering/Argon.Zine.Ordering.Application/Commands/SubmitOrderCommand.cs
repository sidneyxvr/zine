using Argon.Zine.Core.Messages;
using System;
using System.Collections.Generic;

namespace Argon.Zine.Ordering.Application.Commands
{
    public record SubmitOrderCommand : Command
    {
        public Guid CustomerId { get; init; }
        public Guid PaymentMethodId { get; init; }
        public Guid RestaurantId { get; init; }
        public string Street { get; init; } = null!;
        public string Number { get; init; } = null!;
        public string District { get; init; } = null!;
        public string City { get; init; } = null!;
        public string State { get; init; } = null!;
        public string Country { get; init; } = null!;
        public string PostalCode { get; init; } = null!;
        public string? Complement { get; init; }
        public IEnumerable<OrderItemDTO> OrderItems { get; init; } = null!;
    }

    public record OrderItemDTO
    {
        public Guid ProductId { get; init; }
        public string ProductName { get; init; } = null!;
        public string ProductImageUrl { get; init; } = null!;
        public decimal UnitPrice { get; init; }
        public int Units { get; init; }
    }
}
