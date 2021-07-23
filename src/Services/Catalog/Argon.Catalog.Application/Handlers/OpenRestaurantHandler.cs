using Argon.Catalog.Domain;
using Argon.Core.Messages;
using Argon.Core.Messages.IntegrationEvents;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Application.Handlers
{
    public class OpenRestaurantHandler : NotificationHandler<OpenRestaurantEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<OpenRestaurantHandler> _logger;

        public OpenRestaurantHandler(
            IUnitOfWork unitOfWork,
            ILogger<OpenRestaurantHandler> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public override async Task Handle(OpenRestaurantEvent notification, CancellationToken cancellationToken)
        {
            var restaurant = await _unitOfWork.RestaurantRepository.GetByIdAsync(notification.AggregateId);

            if(restaurant is null)
            {
                _logger.LogWarning($"Cannot open restaurant {notification.AggregateId}", 
                    $"Handler {nameof(OpenRestaurantHandler)}",
                    $"Assembly {typeof(OpenRestaurantHandler).Assembly}",
                    notification);

                return;
            }

            restaurant.Open();

            await _unitOfWork.RestaurantRepository.UpdateAsync(restaurant);
            await _unitOfWork.CommitAsync();
        }
    }
}
