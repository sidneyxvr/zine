using Argon.Zine.Core.Messages;
using Argon.Zine.Core.Messages.IntegrationEvents;
using Argon.Restaurants.Application.Commands;
using Argon.Restaurants.Domain;
using FluentValidation.Results;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using System.Threading;

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

    public override async Task<ValidationResult> Handle(
        CloseRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restarutant = await _unitOfWork.RestaurantRepository
            .GetByIdAsync(request.RestaurantId, Include.None, cancellationToken);

        if (restarutant is null)
        {
            return WithError(nameof(restarutant), _localizer["Restaurant Not Found"]);
        }

        restarutant.Close();
        restarutant.AddDomainEvent(new ClosedRestaurantEvent(restarutant.Id));

        await _unitOfWork.RestaurantRepository.UpdateAsync(restarutant, cancellationToken);
        await _unitOfWork.CommitAsync();

        return ValidationResult;
    }
}