using Argon.Zine.Identity.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Argon.Zine.App.Api.Tests.Setup;

public static class IdentitySetup
{
    public static IServiceProvider CreateIdentityDatabase(this IServiceProvider services)
    {
        var identityContext = services.GetRequiredService<IdentityContext>();
        identityContext.Database.EnsureDeleted();
        identityContext.Database.EnsureCreated();

        return services;
    }
}