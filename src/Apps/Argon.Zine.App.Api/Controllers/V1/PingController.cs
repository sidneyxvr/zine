using Argon.Restaurants.Infra.Data;
using Argon.Zine.Catalog.Infra.Data;
using Argon.Zine.Customers.Infra.Data;
using Argon.Zine.Identity.Data;
using Argon.Zine.Ordering.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Argon.Zine.App.Api.Controllers.V1;

[ApiController]
[AllowAnonymous]
[Route("api/ping")]
public class PingController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;

    public PingController(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    [HttpGet]
    public IActionResult Ping()
        => Ok("V1");

    [HttpPost("migrations")]
    public async Task<IActionResult> RunMigrationsAsync()
    {
        var scope = _serviceProvider.CreateScope();

        var catalogContext = scope.ServiceProvider.GetRequiredService<CatalogContext>();
        var customerContext = scope.ServiceProvider.GetRequiredService<CustomerContext>();
        var identityContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
        var oderingContext = scope.ServiceProvider.GetRequiredService<OrderingContext>();
        var restaurantContext = scope.ServiceProvider.GetRequiredService<RestaurantContext>();

        await catalogContext.Database.EnsureCreatedAsync();
        await customerContext.Database.EnsureCreatedAsync();
        await identityContext.Database.EnsureCreatedAsync();
        await oderingContext.Database.EnsureCreatedAsync();
        await restaurantContext.Database.EnsureCreatedAsync();

        return Ok();
    }
}