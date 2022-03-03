using Argon.Zine.Basket.Services;
using Argon.Zine.Commom.Messages;
using Argon.Zine.Commom.Messages.IntegrationEvents;
using Microsoft.Extensions.Logging;

namespace Argon.Zine.Basket.Handlers;

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
            const string errorMessageLog =
                "Cannot update basket item price - item id: {AggregationId}\n{Handler}\n{Assembly}\n{@Notification}";
            _logger.LogWarning(errorMessageLog,
                notification.AggregateId,
                nameof(ChangedProductPriceHandler),
                typeof(ChangedProductPriceHandler).Assembly,
                notification);
        }
    }
}