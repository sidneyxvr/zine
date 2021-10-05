namespace Argon.Zine.Catalog.Shared.Response;

public record RestaurantDetailsResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string? LogoUrl { get; set; }
}
