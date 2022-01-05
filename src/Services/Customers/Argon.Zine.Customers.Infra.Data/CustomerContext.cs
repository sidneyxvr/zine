using Argon.Zine.Commom.Messages;
using Argon.Zine.Customers.Domain;
using Microsoft.EntityFrameworkCore;

namespace Argon.Zine.Customers.Infra.Data;

public class CustomerContext : DbContext
{
    public CustomerContext(DbContextOptions<CustomerContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Event>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerContext).Assembly);
    }

    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
}