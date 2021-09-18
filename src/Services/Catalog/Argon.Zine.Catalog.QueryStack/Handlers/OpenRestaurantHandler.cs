using Argon.Zine.Catalog.QueryStack.Services;
using Argon.Zine.Core.Messages;
using Argon.Zine.Core.Messages.IntegrationEvents;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Zine.Catalog.QueryStack.Handlers
{
    public class OpenRestaurantHandler : NotificationHandler<OpenRestaurantEvent>
    {
        private readonly IRestaurantService _restaurantService;
        private readonly ILogger<OpenRestaurantHandler> _logger;

        public OpenRestaurantHandler(
            IRestaurantService restaurantService,
            ILogger<OpenRestaurantHandler> logger)
        {
            _logger = logger;
            _restaurantService = restaurantService;
        }

        public override async Task Handle(OpenRestaurantEvent notification, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantService.GetByIdAsync(notification.AggregateId);

            if (restaurant is null)
            {
                _logger.LogWarning($"Cannot open restaurant {notification.AggregateId}",
                    $"Handler {nameof(OpenRestaurantHandler)}",
                    $"Assembly {typeof(OpenRestaurantHandler).Assembly}",
                    notification);

                return;
            }

            restaurant.Open();

            await _restaurantService.UpdateAsync(restaurant);
        }
    }
}