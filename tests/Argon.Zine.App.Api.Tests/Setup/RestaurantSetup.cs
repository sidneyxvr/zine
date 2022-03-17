using Argon.Restaurants.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Argon.Zine.App.Api.Tests.Setup;

public static class RestaurantSetup
{
    public static IServiceProvider CreateRestaurantDatabase(this IServiceProvider services)
    {
        var restaurantContext = services.GetRequiredService<RestaurantContext>();
        restaurantContext.Database.EnsureDeleted();
        restaurantContext.Database.EnsureCreated();

        SeedRestaurantDatabase(restaurantContext);

        return services;
    }

    public static void SeedRestaurantDatabase(RestaurantContext context)
    {
        context.Database.ExecuteSqlRaw(@"INSERT INTO public.""Address"" (""Id"", ""Street"", ""Number"", ""District"", ""City"", ""State"", ""Country"", ""PostalCode"", ""Complement"", ""Location"", ""RestaurantId"") VALUES ('814cc253-43f2-4bb1-8496-6c6f39d02e4e', 'Street 1', '100', 'District 1', 'City 1', 'AA', 'Brasil', '60000000', NULL, '0101000020E61000002E90A0F831E6F53F083D9B559F8B3E40', NULL);");
        context.Database.ExecuteSqlRaw(@"INSERT INTO public.""Restaurant"" (""Id"", ""CorporateName"", ""TradeName"", ""IsActive"", ""IsDeleted"", ""IsSuspended"", ""IsOpen"", ""CPFCNPJ"", ""AddressId"", ""LogoUrl"", ""CreatedAt"") VALUES ('02da557e-6b12-4eec-bb36-cd11b529831b', 'Test', 'Test', true, false, false, false, '85115431000156', '814cc253-43f2-4bb1-8496-6c6f39d02e4e', NULL, '2022-03-17 03:55:08.026483+00');");
        context.Database.ExecuteSqlRaw(@"INSERT INTO public.""User"" (""Id"", ""FirstName"", ""LastName"", ""Email"", ""IsActive"", ""IsDelete"", ""RestaurantId"", ""CreatedAt"") VALUES ('7df063d1-52b3-48ce-4dec-08da07c9ece1', 'Test', 'Test', 'confirmed_email@email.com', true, false, '02da557e-6b12-4eec-bb36-cd11b529831b', '2022-03-17 03:55:08.028039+00');");
    }
}