using Argon.Zine.Commom;
using Argon.Zine.Commom.DomainObjects;
using Argon.Zine.Commom.Messages;
using Argon.Zine.Customers.Application.Commands;
using Argon.Zine.Customers.Domain;

namespace Argon.Zine.Customers.Application.Handlers;

public class CreateAddressHandler : RequestHandler<CreateAddressCommand>
{
    private readonly IAppUser _appUser;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAddressHandler(IUnitOfWork unitOfWork, IAppUser appUser)
    {
        _appUser = appUser;
        _unitOfWork = unitOfWork;
    }

    public override async Task<AppResult> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.CustomerRepository
            .GetByIdAsync(_appUser.Id, Includes.None, cancellationToken);

        if (customer is null)
        {
            throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
        }

        var location = new Location(request.Latitude!.Value, request.Longitude!.Value);
        var address = new Address(request.Street, request.Number, request.District, 
            request.City, request.State, request.PostalCode, location);

        customer.AddAddress(address);

        await _unitOfWork.CustomerRepository.UpdateAsync(customer, cancellationToken);
        await _unitOfWork.CommitAsync();

        return customer;
    }
}