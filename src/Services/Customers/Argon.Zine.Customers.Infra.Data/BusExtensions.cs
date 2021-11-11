using Argon.Zine.Core.Communication;
using Argon.Zine.Core.DomainObjects;
using System.Linq;
using System.Threading.Tasks;

namespace Argon.Zine.Customers.Infra.Data
{
    public static class BusExtensions
    {
        public static async Task PublishEventsAsync(this IBus bus, CustomerContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await bus.PublishAsync(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
