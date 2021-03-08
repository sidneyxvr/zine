using Argon.Customers.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Customers.Application.EventHandlers.CustomersHandlers
{
    public class CreatedCustomerHandler : INotificationHandler<CreatedCustomerEvent>
    {
        public async Task Handle(CreatedCustomerEvent notification, CancellationToken cancellationToken)
        {
            await Task.Delay(500, cancellationToken);
        }
    }
}
