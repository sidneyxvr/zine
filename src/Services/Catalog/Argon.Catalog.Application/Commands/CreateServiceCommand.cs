using Argon.Core.Messages;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Argon.Catalog.Application.Commands
{
    public record CreateServiceCommand : Command 
    {
        public string? Name { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
        public Guid SubCategoryId { get; init; }
        public Guid SupplierId { get; init; }
        public bool HasHomeAssistance { get; init; }
        public IEnumerable<ImageDTO>? Images { get; init; }
        public IEnumerable<FeeHomeAssistenceDTO>? FeeHomeAssistences { get; init; }
        public IEnumerable<Guid>? Tags { get; init; }
    }

    public class ImageDTO 
    {
        public IFormFile? Image { get; init; }
        public int Order { get; init; }
    }

    public class FeeHomeAssistenceDTO
    {
        public decimal Price { get; set; }
        public double Radius { get; set; }
    }
}
