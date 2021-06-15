using Argon.Core.Messages;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Argon.Catalog.Application.Commands
{
    public class CreateServiceCommand : Command 
    {
        public string Name { get; init; }
        public decimal Price { get; init; }
        public Guid CategoryId { get; init; }
        public List<ImageDTO> Images { get; init; }
    }

    public class ImageDTO 
    {
        public IFormFile Image { get; init; }
        public int Order { get; init; }
    }

}
