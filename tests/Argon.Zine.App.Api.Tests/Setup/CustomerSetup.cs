using Argon.Zine.Customers.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Argon.Zine.App.Api.Tests.Setup;

public static class CustomerSetup
{
    public static IServiceProvider CreateCustomerDatabase(this IServiceProvider services)
    {
        var customerContext = services.GetRequiredService<CustomerContext>();
        customerContext.Database.EnsureDeleted();
        customerContext.Database.EnsureCreated();

        SeedIdentityDatabase(customerContext);

        return services;
    }

    public static void SeedIdentityDatabase(CustomerContext context)
    {
        context.Database.ExecuteSqlRaw(@"INSERT INTO public.""Customer"" (""Id"", ""FirstName"", ""Surname"", ""Email"", ""CPF"", ""BirthDate"", ""Phone"", ""IsActive"", ""IsDeleted"", ""IsSuspended"", ""MainAddressId"", ""CreatedAt"") VALUES ('0a118de1-08b3-4eac-8204-08da07c1a929', 'Test', 'Test', 'non_confirmed_email@email.com', '43623748099', '2000-03-17', NULL, true, false, true, NULL, '2022-03-17 02:55:58.511054+00');");
    }
}