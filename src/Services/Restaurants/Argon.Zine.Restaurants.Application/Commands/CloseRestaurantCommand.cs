using Argon.Zine.Core.Messages;

namespace Argon.Restaurants.Application.Commands;

public record CloseRestaurantCommand : Command
{
    public Guid RestaurantId { get; init; }
}