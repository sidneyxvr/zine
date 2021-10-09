namespace Argon.Zine.App.Web.ViewModels.Product;

public class ProductGridItemViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public double Price { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; }
}