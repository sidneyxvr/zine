using Argon.Zine.Customers.Infra.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Argon.Zine.App.Api.Tests.Setup;

public static class CustomerSetup
{
    public static IServiceProvider CreateCustomerDatabase(this IServiceProvider services)
    {
        var customerContext = services.GetRequiredService<CustomerContext>();
        customerContext.Database.EnsureDeleted();
        customerContext.Database.EnsureCreated();

        return services;
    }
}