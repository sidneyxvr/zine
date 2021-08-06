using Argon.Core.Messages;
using Argon.Core.Messages.IntegrationEvents;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Basket.Handlers
{
    public class ChangedProductPriceHandler : NotificationHandler<ChangedProductPriceEvent>
    {
        public override Task Handle(ChangedProductPriceEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
