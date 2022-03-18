using Argon.Zine.Commom;
using Argon.Zine.Commom.Messages;
using Argon.Zine.Ordering.Application.Commands;
using Argon.Zine.Ordering.Domain;

namespace Argon.Zine.Ordering.Application.Handlers;

public class SubmitOrderHandler : RequestHandler<SubmitOrderCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISequencialIdentifier _sequenciaIdentifier;

    public SubmitOrderHandler(
        IUnitOfWork unitOfWork,
        ISequencialIdentifier sequenciaIdentifier)
    {
        _unitOfWork = unitOfWork;
        _sequenciaIdentifier = sequenciaIdentifier;
    }

    public override async Task<AppResult> Handle(SubmitOrderCommand request, CancellationToken cancellationToken)
    {
        var addressDto = request.Address;
        var address = new Address(addressDto.Street, addressDto.Number, addressDto.District, addressDto.District,
            addressDto.State, addressDto.Country, addressDto.PostalCode, addressDto.Complement);

        var orderItems = request.OrderItems
            .Select(o => new OrderItem(o.ProductId, o.ProductName, o.ProductImageUrl, o.UnitPrice, o.Units))
            .ToList();

        var sequentialId = await _sequenciaIdentifier.GetSequentialIdAsync();
        var order = Order.SubmitOrder(request.CustomerId, request.PaymentMethodId,
            sequentialId, request.RestaurantId, address, orderItems);

        await _unitOfWork.OrderRepository.AddAsync(order, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return order;
    }
}