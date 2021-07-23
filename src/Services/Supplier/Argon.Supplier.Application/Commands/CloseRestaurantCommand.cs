using Argon.Core.Messages;
using System;

namespace Argon.Restaurants.Application.Commands
{
    public record CloseRestaurantCommand : Command
    {
        public Guid RestaurantId { get; init; }
    }
}
