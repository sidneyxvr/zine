using Argon.Core.Messages;
using System;

namespace Argon.Restaurants.Application.Commands
{
    public record OpenRestaurantCommand : Command
    {
        public Guid RestaurantId { get; init; }
    }
}
