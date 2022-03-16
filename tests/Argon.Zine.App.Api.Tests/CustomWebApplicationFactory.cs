using Amazon.S3;
using Amazon.S3.Transfer;
using Argon.Zine.App.Api.Tests.FakeServices;
using Argon.Zine.App.Api.Tests.Setup;
using Argon.Zine.Commom.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Argon.Zine.App.Api.Tests;

public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Staging");

        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(AmazonS3Client));
            services.RemoveAll(typeof(TransferUtility));

            var fileStorage = ServiceDescriptor.Scoped<IFileStorage, FakeFileStorage>();
            services.Replace(fileStorage);

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();

            scope.ServiceProvider.CreateCatalogDatabase();
            scope.ServiceProvider.CreateIdentityDatabase();
            scope.ServiceProvider.CreateCustomerDatabase();
            scope.ServiceProvider.CreateRestaurantDatabase();
        });
    }
}