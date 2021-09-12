using Argon.Zine.Core.Communication;
using Argon.Zine.Core.DomainObjects;
using System.Linq;
using System.Threading.Tasks;

namespace Argon.Zine.Catalog.Infra.Data
{
    public static class BusExtensions
    {
        public static async Task PublishAllAsync(this IBus bus, CatalogContext ctx)
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
                .Select(async (domainEvent) => {
                    await bus.PublishAsync(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
