using Argon.Zine.Basket.Services;
using Argon.Zine.Core.Messages;
using Argon.Zine.Core.Messages.IntegrationEvents;
using Microsoft.Extensions.Logging;

namespace Argon.Zine.Basket.Handlers
{
    public class ChangedProductPriceHandler : NotificationHandler<ChangedProductPriceEvent>
    {
        private readonly IBasketService _basketService;
        private readonly ILogger<ChangedProductPriceHandler> _logger;

        public ChangedProductPriceHandler(
            IBasketService basketService, 
            ILogger<ChangedProductPriceHandler> logger)
        {
            _logger = logger;
            _basketService = basketService;
        }

        public override async Task Handle(ChangedProductPriceEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                await _basketService.UpdateBasketItemPriceAsync(notification.AggregateId, notification.Price);
            }
            catch
            {
                _logger.LogWarning($"Cannot update basket item price - item id: {notification.AggregateId}",
                   $"Handler {nameof(ChangedProductPriceHandler)}",
                   $"Assembly {typeof(ChangedProductPriceHandler).Assembly}",
                   notification);
            }
        }
    }
}
