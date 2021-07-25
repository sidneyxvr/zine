using Argon.Core.DomainObjects;
using System;
using System.Collections.Generic;

namespace Argon.Ordering.Domain
{
    public class Order : Entity, IAggregateRoot
    {
        public Guid BuyerId { get; private set; }
        public Address Address { get; private set; }
        public DateTime OrderedAt { get; private set; }
        public Guid PaymentMethodId { get; private set; }

        public Guid RestaurantId { get; private set; }
        public OrderStatus CurrentOrderStatus { get; private set; }

        private List<OrderItem> _orderItems = new();
        public IReadOnlyCollection<OrderItem> OrderItems
            => _orderItems.AsReadOnly();

        private List<OrderStatus> _orderStatuses = new();
        public IReadOnlyCollection<OrderStatus> OrderStatuses
            => _orderStatuses.AsReadOnly();

#pragma warning disable CS8618
        private Order() { }
#pragma warning restore CS8618 

        private Order(Guid buyerId, Guid paymentMethodId, Guid restaurantId,
            Address address, List<OrderItem> orderItems)
        {
            BuyerId = buyerId;
            Address = address;
            OrderedAt = DateTime.UtcNow;
            PaymentMethodId = paymentMethodId;
            RestaurantId = restaurantId;

            CurrentOrderStatus = OrderStatus.Submitted;
            _orderStatuses.Add(CurrentOrderStatus);

            _orderItems = orderItems;
        }

        public static Order SubmitOrder(Guid buyerId, Guid paymentMethodId,
            Guid restaurantId, Address address, List<OrderItem> orderItems)
            => new(buyerId, paymentMethodId, restaurantId, address, orderItems);
    }
}
