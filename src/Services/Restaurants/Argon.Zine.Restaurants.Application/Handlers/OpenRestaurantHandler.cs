using Argon.Restaurants.Application.Commands;
using Argon.Restaurants.Domain;
using Argon.Zine.Commom;
using Argon.Zine.Commom.Messages;
using Argon.Zine.Commom.Messages.IntegrationEvents;
using Microsoft.Extensions.Localization;

namespace Argon.Restaurants.Application.Handlers;

public class OpenRestaurantHandler : RequestHandler<OpenRestaurantCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStringLocalizer<OpenRestaurantHandler> _localizer;
    
    public OpenRestaurantHandler(
        IUnitOfWork unitOfWork,
        IStringLocalizer<OpenRestaurantHandler> localizer)
    {
        _localizer = localizer;
        _unitOfWork = unitOfWork;
    }

    public override async Task<AppResult> Handle(
        OpenRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await _unitOfWork.RestaurantRepository
            .GetByIdAsync(request.RestaurantId, Includes.None, cancellationToken);

        if (restaurant is null)
        {
            return WithError(nameof(restaurant), _localizer["Restaurant Not Found"]);
        }

        restaurant.Open();
        restaurant.AddDomainEvent(new OpenRestaurantEvent(restaurant.Id));

        await _unitOfWork.RestaurantRepository.UpdateAsync(restaurant, cancellationToken);
        await _unitOfWork.CommitAsync();

        return restaurant;
    }
}