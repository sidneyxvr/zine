﻿using Argon.Customers.Infra.Data;
using Argon.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Argon.WebApp.Tests.Configuration
{
    public class ArgonAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {


        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                var identityContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<IdentityContext>));
                services.Remove(identityContextDescriptor);

                var customerContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<CustomerContext>));
                services.Remove(customerContextDescriptor);

                services.AddDbContext<IdentityContext>(options =>
                {
                    options.UseSqlite(@"Filename=..\identity-context.db");
                });

                services.AddDbContext<CustomerContext>(options =>
                {
                    options.UseSqlite(@"Filename=..\customer-context.db");
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;

                var customerContext = scopedServices.GetRequiredService<CustomerContext>();
                customerContext.Database.EnsureDeleted();
                customerContext.Database.EnsureCreated();

                var identityContext = scopedServices.GetRequiredService<IdentityContext>();
                identityContext.Database.EnsureDeleted();
                identityContext.Database.EnsureCreated();
                identityContext.Seed();
            });
        }
    }
}
