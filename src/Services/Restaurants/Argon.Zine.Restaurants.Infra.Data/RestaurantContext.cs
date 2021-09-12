using Argon.Zine.Core.Messages;
using Argon.Restaurants.Domain;
using Microsoft.EntityFrameworkCore;

namespace Argon.Restaurants.Infra.Data;

public class RestaurantContext : DbContext
{
    public RestaurantContext(DbContextOptions<RestaurantContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Event>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestaurantContext).Assembly);
    }

    public DbSet<Address> Addresses { get; } = null!;
    public DbSet<Restaurant> Restaurants { get; } = null!;
    public DbSet<User> Users { get; } = null!;
}