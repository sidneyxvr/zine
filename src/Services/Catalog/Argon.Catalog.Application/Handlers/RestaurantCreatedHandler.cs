using Argon.Catalog.Domain;
using Argon.Core.Messages;
using Argon.Core.Messages.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Application.Handlers
{
    public class RestaurantCreatedHandler : NotificationHandler<RestaurantCreatedEvent>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RestaurantCreatedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task Handle(RestaurantCreatedEvent notification, CancellationToken cancellationToken)
        {
            var supplier = new Restaurant(notification.AggregateId, notification.Name, 
                notification.Latitude, notification.Latitude, notification.Address, notification.LogoUrl);

            await _unitOfWork.RestaurantRepository.AddAsync(supplier);
            await _unitOfWork.CommitAsync();
        }
    }
}
