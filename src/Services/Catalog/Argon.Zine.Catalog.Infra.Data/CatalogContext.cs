using Argon.Zine.Catalog.Domain;
using Argon.Zine.Core.Messages;
using Microsoft.EntityFrameworkCore;

namespace Argon.Zine.Catalog.Infra.Data
{
    public class CatalogContext : DbContext
    {
#pragma warning disable CS8618 
        public CatalogContext(DbContextOptions<CatalogContext> options)
#pragma warning restore CS8618 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
