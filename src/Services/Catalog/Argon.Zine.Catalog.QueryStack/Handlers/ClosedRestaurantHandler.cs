using Argon.Zine.Catalog.QueryStack.Services;
using Argon.Zine.Core.Messages;
using Argon.Zine.Core.Messages.IntegrationEvents;
using Microsoft.Extensions.Logging;

namespace Argon.Zine.Catalog.QueryStack.Handlers
{
    public class ClosedRestaurantHandler : NotificationHandler<ClosedRestaurantEvent>
    {
        private readonly IRestaurantService _restaurantService;
        private readonly ILogger<OpenRestaurantHandler> _logger;

        public ClosedRestaurantHandler(
            IRestaurantService restaurantService,
            ILogger<OpenRestaurantHandler> logger)
        {
            _logger = logger;
            _restaurantService = restaurantService;
        }

        public override async Task Handle(ClosedRestaurantEvent notification, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantService.GetByIdAsync(notification.AggregateId);

            if (restaurant is null)
            {
                _logger.LogWarning($"Cannot close restaurant {notification.AggregateId}",
                    $"Handler {nameof(ClosedRestaurantHandler)}",
                    $"Assembly {typeof(ClosedRestaurantHandler).Assembly}",
                    notification);

                return;
            }

            restaurant.Close();

            await _restaurantService.UpdateAsync(restaurant);
        }
    }
}
