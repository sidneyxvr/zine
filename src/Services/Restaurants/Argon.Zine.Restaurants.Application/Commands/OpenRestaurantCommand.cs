using Argon.Zine.Commom.Messages;

namespace Argon.Restaurants.Application.Commands;

public record OpenRestaurantCommand(Guid RestaurantId) : Command;