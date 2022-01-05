using Argon.Zine.Commom.Communication;
using Argon.Zine.Commom.DomainObjects;

namespace Argon.Restaurants.Infra.Data;

public static class BusExtensions
{
    public static async Task PublishEventsAsync(this IBus bus, RestaurantContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents?.Count != 0);

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        var tasks = domainEvents
            .Select(async domainEvent =>
            {
                await bus.PublishAsync(domainEvent);
            });

        await Task.WhenAll(tasks);
    }
}