using Argon.Zine.Commom;
using Argon.Zine.Commom.DomainObjects;
using Argon.Zine.Commom.Messages;
using Argon.Zine.Customers.Application.Commands;
using Argon.Zine.Customers.Domain;
using Microsoft.Extensions.Localization;

namespace Argon.Zine.Customers.Application.Handlers;

public class UpdateAddressHandler : RequestHandler<UpdateAddressCommand>
{
    private readonly IAppUser _appUser;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStringLocalizer _localizer;

    public UpdateAddressHandler(
        IAppUser appUser,
        IUnitOfWork unitOfWork,
        IStringLocalizer<UpdateAddressHandler> localizer)
    {
        _appUser = appUser;
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public override async Task<AppResult> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.CustomerRepository
            .GetByIdAsync(_appUser.Id, Includes.Addresses, cancellationToken);

        if (customer is null)
        {
            throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
        }

        var address = customer.Addresses.FirstOrDefault(a => a.Id == request.AddressId);

        if (address is null)
        {
            return WithError(nameof(address), _localizer["Address Not Found"]);
        }

        var location = new Location(request.Latitude!.Value, request.Longitude!.Value);
        address.Update(request.Street!, request.Number!, request.District!,
            request.City!, request.State!, request.PostalCode!, location);
        address.SetComplement(request.Complement);

        await _unitOfWork.CustomerRepository.UpdateAsync(customer, cancellationToken);
        await _unitOfWork.CommitAsync();

        return customer;
    }
}