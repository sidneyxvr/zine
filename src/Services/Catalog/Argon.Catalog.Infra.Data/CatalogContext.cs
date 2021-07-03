using Argon.Catalog.Domain;
using Argon.Core.Messages;
using Microsoft.EntityFrameworkCore;

namespace Argon.Catalog.Infra.Data
{
    public class CatalogContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public CatalogContext(DbContextOptions<CatalogContext> options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Service> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
