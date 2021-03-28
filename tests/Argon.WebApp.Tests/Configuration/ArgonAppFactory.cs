using Argon.Customers.Infra.Data;
using Argon.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
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
                services.AddDbContext<IdentityContext>(options =>
                {
                    options.UseInMemoryDatabase(nameof(IdentityContext));
                });

                services.AddDbContext<CustomerContext>(options =>
                {
                    options.UseInMemoryDatabase(nameof(CustomerContext));
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<IdentityContext>();

                context.Seed();
            });
        }

        protected override IWebHostBuilder CreateWebHostBuilder() 
            => base.CreateWebHostBuilder().UseEnvironment("Testing");
    }
}
