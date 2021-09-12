using Argon.Zine.Catalog.QueryStack.Services;
using Argon.Zine.Core.Messages;
using Argon.Zine.Core.Messages.IntegrationEvents;

namespace Argon.Zine.Catalog.QueryStack.Handlers
{
    public class RestaurantCreatedHandler : NotificationHandler<RestaurantCreatedEvent>
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantCreatedHandler(IRestaurantService restaurantService)
            => _restaurantService = restaurantService;

        public override async Task Handle(RestaurantCreatedEvent notification, CancellationToken cancellationToken)
            => await _restaurantService.AddAsync(
                new(notification.AggregateId, notification.Name, notification.Address, notification.LogoUrl));
    }
}
