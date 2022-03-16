using Argon.Restaurants.Domain;
using Argon.Zine.Commom.Messages;
using Microsoft.EntityFrameworkCore;

namespace Argon.Restaurants.Infra.Data;

#pragma warning disable CS8618 
public class RestaurantContext : DbContext
{
    public RestaurantContext(DbContextOptions<RestaurantContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");

        modelBuilder.Ignore<Event>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestaurantContext).Assembly);
    }

    public DbSet<Address> Addresses { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<User> Users { get; set; }
}