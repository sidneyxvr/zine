namespace Argon.Zine.Catalog.QueryStack.Responses;

public class ProductItemGridResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public decimal Price { get; init; }
    public bool IsActive { get; init; }
    public string ImageUrl { get; init; } = null!;
}