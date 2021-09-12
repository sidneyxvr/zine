using Argon.Zine.Core.Messages;

namespace Argon.Restaurants.Application.Commands;

public record OpenRestaurantCommand : Command
{
    public Guid RestaurantId { get; init; }
}