using Argon.Core.Messages;
using Argon.Suppliers.Domain;
using Microsoft.EntityFrameworkCore;

namespace Argon.Suppliers.Infra.Data
{
    public class SupplierContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public SupplierContext(DbContextOptions<SupplierContext> options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    :        base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SupplierContext).Assembly);
        }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
