using Argon.Core.Communication;
using Argon.Core.DomainObjects;
using System.Linq;
using System.Threading.Tasks;

namespace Argon.Suppliers.Infra.Data
{
    public static class BusExtensions
    {
        public static async Task PublicarEventos(this IBus bus, SupplierContext ctx)
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
