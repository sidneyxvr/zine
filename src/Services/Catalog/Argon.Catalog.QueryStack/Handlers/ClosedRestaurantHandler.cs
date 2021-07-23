using Argon.Catalog.QueryStack.Cache;
using Argon.Catalog.QueryStack.Services;
using Argon.Core.Messages;
using Argon.Core.Messages.IntegrationEvents;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.QueryStack.Handlers
{
    public class ClosedRestaurantHandler : NotificationHandler<ClosedRestaurantEvent>
    {
        private readonly IRestaurantCache _restaurantCache;
        private readonly IRestaurantService _restaurantService;
        private readonly ILogger<OpenRestaurantHandler> _logger;

        public ClosedRestaurantHandler(
            IRestaurantCache restaurantCache,
            IRestaurantService restaurantService,
            ILogger<OpenRestaurantHandler> logger)
        {
            _logger = logger;
            _restaurantCache = restaurantCache;
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

            await _restaurantCache.DeleteAsync(restaurant.Id);
            await _restaurantService.UpdateAsync(restaurant);
        }
    }
}
