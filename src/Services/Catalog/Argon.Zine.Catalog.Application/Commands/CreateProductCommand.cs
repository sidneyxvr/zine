using Argon.Zine.Core.Messages;
using Microsoft.AspNetCore.Http;
using System;

namespace Argon.Zine.Catalog.Application.Commands
{
    public record CreateProductCommand : Command 
    {
        public string? Name { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
        public Guid RestaurantId { get; init; }
        public IFormFile? Image { get; init; }
    }
}
