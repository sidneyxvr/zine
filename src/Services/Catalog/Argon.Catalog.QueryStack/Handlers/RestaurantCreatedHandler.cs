﻿using Argon.Catalog.QueryStack.Services;
using Argon.Core.Messages;
using Argon.Core.Messages.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.QueryStack.Handlers
{
    public class RestaurantCreatedHandler : NotificationHandler<RestaurantCreatedEvent>
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantCreatedHandler(IRestaurantService restaurantService)
            => _restaurantService = restaurantService;

        public override async Task Handle(RestaurantCreatedEvent notification, CancellationToken cancellationToken)
            => await _restaurantService.AddAsync(new(notification.AggregateId, notification.Name, notification.Address));
    }
}
