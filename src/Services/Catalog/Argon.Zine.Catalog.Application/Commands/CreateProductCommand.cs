using Argon.Zine.Commom.Messages;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Argon.Zine.Catalog.Application.Commands;

public record CreateProductCommand : Command
{
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public decimal Price { get; init; }
    [JsonIgnore]
    public Guid RestaurantId { get; private set; }
    public IFormFile? Image { get; init; }
    public bool IsActive { get; set; }

    public void SetRestaurantId(Guid restaurantId)
        => RestaurantId = restaurantId;
}