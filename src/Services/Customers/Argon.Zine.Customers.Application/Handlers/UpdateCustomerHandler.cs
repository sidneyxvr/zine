using Argon.Zine.Commom;
using Argon.Zine.Commom.DomainObjects;
using Argon.Zine.Commom.Messages;
using Argon.Zine.Customers.Application.Commands;
using Argon.Zine.Customers.Domain;

namespace Argon.Zine.Customers.Application.Handlers;

public class UpdateCustomerHandler : RequestHandler<UpdateCustomerCommand>
{
    private readonly IAppUser _appUser;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCustomerHandler(
        IAppUser appUser,
        IUnitOfWork unitOfWork)
    {
        _appUser = appUser;
        _unitOfWork = unitOfWork;
    }

    public override async Task<AppResult> Handle(
        UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.CustomerRepository
            .GetByIdAsync(_appUser.Id, Includes.None, cancellationToken);

        if (customer is null)
        {
            throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
        }

        var name = new Name(request.FirstName!, request.LastName!);
        customer.Update(name, request.BirthDate);

        await _unitOfWork.CustomerRepository.UpdateAsync(customer, cancellationToken);
        await _unitOfWork.CommitAsync();

        return customer;
    }
}