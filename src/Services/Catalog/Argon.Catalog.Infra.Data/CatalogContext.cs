using Argon.Catalog.Domain;
using Argon.Core.Messages;
using Microsoft.EntityFrameworkCore;

namespace Argon.Catalog.Infra.Data
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options)
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
