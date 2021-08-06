﻿using Argon.Core.Messages;
using Argon.Restaurants.Domain;
using Microsoft.EntityFrameworkCore;

namespace Argon.Restaurants.Infra.Data
{
    public class RestaurantContext : DbContext
    {
#pragma warning disable CS8618 
        public RestaurantContext(DbContextOptions<RestaurantContext> options)
#pragma warning restore CS8618 
    :        base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestaurantContext).Assembly);
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<User> Users { get; set; }
    }
}