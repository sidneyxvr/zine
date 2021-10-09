namespace Argon.Zine.Catalog.QueryStack.Responses;

public record RestaurantDetailsResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string? LogoUrl { get; set; }
}
