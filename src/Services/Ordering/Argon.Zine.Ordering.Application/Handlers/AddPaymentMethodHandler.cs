using Argon.Zine.Commom;
using Argon.Zine.Commom.Messages;
using Argon.Zine.Ordering.Application.Commands;
using Argon.Zine.Ordering.Domain;

namespace Argon.Zine.Ordering.Application.Handlers;

public class AddPaymentMethodHandler : RequestHandler<AddPaymentMethodCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddPaymentMethodHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public override async Task<AppResult> Handle(AddPaymentMethodCommand request, CancellationToken cancellationToken)
    {
        var buyer = await _unitOfWork.BuyerRepository.GetByIdAsync(request.CustomerId, cancellationToken);

        var buyerWasNull = buyer is null;

        buyer ??= new(request.CustomerId, request.CustomerFirstName, request.CustomerLastName);

        var paymentMethod = new PaymentMethod(request.Alias, request.CardNamber, request.CardHolderName, request.Expiration);

        buyer.AddPaymentMethod(paymentMethod);

        if (buyerWasNull)
        {
            await _unitOfWork.BuyerRepository.AddAsync(buyer, cancellationToken);
        }
        else
        {
            await _unitOfWork.BuyerRepository.UpdateAsync(buyer, cancellationToken);
        }

        await _unitOfWork.CommitAsync(cancellationToken);

        return buyer;
    }
}