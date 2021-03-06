using Argon.Zine.Commom.Messages;
using System;

namespace Argon.Restaurants.Application.Commands;

public record CloseRestaurantCommand : Command
{
    public Guid RestaurantId { get; init; }
}