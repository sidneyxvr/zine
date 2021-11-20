using Argon.Zine.Core.Messages;
using Argon.Zine.Ordering.Domain;
using Microsoft.EntityFrameworkCore;

namespace Argon.Zine.Ordering.Infra.Data;

public class OrderingContext : DbContext
{
    public OrderingContext(DbContextOptions<OrderingContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Event>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderingContext).Assembly);
    }

    public DbSet<Buyer> Buyers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    public DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
    public DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
    public DbSet<Restaurant> Restaurants { get; set; } = null!;
}