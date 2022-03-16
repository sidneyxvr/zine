using Argon.Zine.Catalog.Domain;
using Argon.Zine.Catalog.Infra.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Argon.Zine.App.Api.Tests.Setup;

public static class CatalogSetup
{
    public static IServiceProvider CreateCatalogDatabase(this IServiceProvider services)
    {
        var catalogContext = services.GetRequiredService<CatalogContext>();
        catalogContext.Database.EnsureDeleted();
        catalogContext.Database.EnsureCreated();

        SeedCatalogDatabase(catalogContext);

        return services;
    }

    public static void SeedCatalogDatabase(CatalogContext context)
    {
        context.Categories.Add(new Category("categoria", "descrição da categoria"));
        context.SaveChanges();
    }
}