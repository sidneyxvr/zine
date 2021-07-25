using Argon.Core.Messages;
using Argon.Ordering.Application.Commands;
using Argon.Ordering.Domain;
using FluentValidation.Results;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Ordering.Application.Handlers
{
    public class SubmitOrderHandler : RequestHandler<SubmitOrderCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubmitOrderHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<ValidationResult> Handle(SubmitOrderCommand request, CancellationToken cancellationToken)
        {
            var address = new Address(request.Street, request.Number, request.District, request.District,
                request.State, request.Country, request.PostalCode, request.Complement);

            var orderItems = request.OrderItems
                .Select(o => new OrderItem(o.ProductId, o.ProductName, o.ProductImageUrl, o.UnitPrice, o.Units))
                .ToList();

            var order = Order.SubmitOrder(request.CustomerId, request.PaymentMethodId, request.RestaurantId, address, orderItems);

            await _unitOfWork.OrderRepository.AddAsync(order, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new();
        }
    }
}
