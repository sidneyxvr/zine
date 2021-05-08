using Argon.Catalog.Domain;
using Argon.Core.Messages.IntegrationEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Application.EventHandlers
{
    public class SupplierCreatedHandler : INotificationHandler<SupplierCreatedEvent>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SupplierCreatedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(SupplierCreatedEvent notification, CancellationToken cancellationToken)
        {
            var supplier = new Supplier(notification.Name, notification.Latitude, notification.Latitude, notification.Address);

            await _unitOfWork.SupplierRepository.AddAsync(supplier);
            await _unitOfWork.CommitAsync();
        }
    }
}
