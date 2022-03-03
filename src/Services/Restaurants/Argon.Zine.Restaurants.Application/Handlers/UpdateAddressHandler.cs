using Argon.Restaurants.Application.Commands;
using Argon.Restaurants.Domain;
using Argon.Zine.Commom;
using Argon.Zine.Commom.DomainObjects;
using Argon.Zine.Commom.Messages;
using FluentValidation.Results;
using Microsoft.Extensions.Localization;

namespace Argon.Restaurants.Application.Handlers;

public class UpdateAddressHandler : RequestHandler<UpdateAddressCommand>
{
    private readonly IAppUser _appUser;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStringLocalizer _localizer;

    public UpdateAddressHandler(
        IAppUser appUser,
        IUnitOfWork unitOfWork,
        IStringLocalizer localizer)
    {
        _appUser = appUser;
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public override async Task<AppResult> Handle(
        UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await _unitOfWork.RestaurantRepository
            .GetByIdAsync(_appUser.Id, Includes.Address, cancellationToken);

        if (restaurant is null)
        {
            throw new ArgumentNullException(nameof(restaurant), "supplier cannot be null");
        }

        var address = restaurant.Address;

        if (address is null)
        {
            return WithError(nameof(address), _localizer["Address not found"]);
        }

        var location = new Location(request.Latitude!.Value, request.Longitude!.Value);
        address.Update(request.Street, request.Number, request.District, 
            request.City, request.State, request.PostalCode, location);
        address.SetComplement(request.Complement);

        await _unitOfWork.RestaurantRepository.UpdateAsync(restaurant, cancellationToken);
        await _unitOfWork.CommitAsync();

        return restaurant;
    }
}