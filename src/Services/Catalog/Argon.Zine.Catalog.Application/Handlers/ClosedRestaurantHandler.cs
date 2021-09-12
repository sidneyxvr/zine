using Argon.Zine.Catalog.Domain;
using Argon.Zine.Core.Messages;
using Argon.Zine.Core.Messages.IntegrationEvents;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Zine.Catalog.Application.Handlers
{
    public class ClosedRestaurantHandler : NotificationHandler<ClosedRestaurantEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<OpenRestaurantHandler> _logger;

        public ClosedRestaurantHandler(
            IUnitOfWork unitOfWork,
            ILogger<OpenRestaurantHandler> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public override async Task Handle(ClosedRestaurantEvent notification, CancellationToken cancellationToken)
        {
            var restaurant = await _unitOfWork.RestaurantRepository.GetByIdAsync(notification.AggregateId);

            if (restaurant is null)
            {
                _logger.LogWarning($"Cannot close restaurant {notification.AggregateId}",
                    $"Handler {nameof(ClosedRestaurantHandler)}",
                    $"Assembly {typeof(ClosedRestaurantHandler).Assembly}",
                    notification);

                return;
            }

            restaurant.Close();

            await _unitOfWork.RestaurantRepository.UpdateAsync(restaurant);
            await _unitOfWork.CommitAsync();
        }
    }
}
