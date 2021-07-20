﻿using Argon.Core.Messages;
using Argon.Core.Messages.IntegrationEvents;
using Argon.Restaurants.Application.Commands;
using Argon.Restaurants.Domain;
using FluentValidation.Results;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Restaurants.Application.Handlers
{
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

        public override async Task<ValidationResult> Handle(
            OpenRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restarutant = await _unitOfWork.RestaurantRepository
                .GetByIdAsync(request.RestaurantId);

            if(restarutant is null)
            {
                return WithError(nameof(restarutant), _localizer["Restaurant Not Found"]);
            }

            restarutant.Open();
            restarutant.AddDomainEvent(new OpenRestaurantEvent(restarutant.Id));

            await _unitOfWork.RestaurantRepository.UpdateAsync(restarutant);
            await _unitOfWork.CommitAsync();

            return ValidationResult;
        }
    }
}
