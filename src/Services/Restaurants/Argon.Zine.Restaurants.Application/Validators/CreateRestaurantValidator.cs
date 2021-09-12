using Argon.Zine.Core.Messages.IntegrationCommands;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Argon.Restaurants.Application.Validators;

public class CreateRestaurantValidator : AbstractValidator<CreateRestaurantCommand>
{
    public CreateRestaurantValidator(IStringLocalizer<CreateRestaurantValidator> localizer)
    {

    }
}