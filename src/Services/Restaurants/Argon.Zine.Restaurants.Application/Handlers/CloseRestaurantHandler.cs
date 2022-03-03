using Argon.Restaurants.Application.Commands;
using Argon.Restaurants.Domain;
using Argon.Zine.Application;
using Argon.Zine.Commom;
using Argon.Zine.Commom.Messages;
using Argon.Zine.Commom.Messages.IntegrationEvents;
using FluentValidation.Results;
using Microsoft.Extensions.Localization;

namespace Argon.Restaurants.Application.Handlers;

public class CloseRestaurantHandler : RequestHandler<CloseRestaurantCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStringLocalizer<CloseRestaurantHandler> _localizer;

    public CloseRestaurantHandler(
        IUnitOfWork unitOfWork,
        IStringLocalizer<CloseRestaurantHandler> localizer)
    {
        _localizer = localizer;
        _unitOfWork = unitOfWork;
    }

    [PermissionValidator(Permission = 1)]
    public override async Task<AppResult> Handle(
        CloseRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restarutant = await _unitOfWork.RestaurantRepository
            .GetByIdAsync(request.RestaurantId, Includes.None, cancellationToken);

        if (restarutant is null)
        {
            return WithError(nameof(restarutant), _localizer["Restaurant Not Found"]);
        }

        restarutant.Close();
        restarutant.AddDomainEvent(new ClosedRestaurantEvent(restarutant.Id));

        await _unitOfWork.RestaurantRepository.UpdateAsync(restarutant, cancellationToken);
        await _unitOfWork.CommitAsync();

        return restarutant;
    }
}