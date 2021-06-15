using Argon.Customers.Infra.Data;
using Argon.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Argon.WebApp.Tests.Configuration
{
    public class ArgonAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            { 
                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var identityContext = scopedServices.GetRequiredService<IdentityContext>();
                identityContext.Database.EnsureDeleted();
                identityContext.Database.EnsureCreated();
                identityContext.Seed();

                var customerContext = scopedServices.GetRequiredService<CustomerContext>();
                customerContext.Database.EnsureDeleted();
                customerContext.Database.EnsureCreated();
            });
        }
    }
}
