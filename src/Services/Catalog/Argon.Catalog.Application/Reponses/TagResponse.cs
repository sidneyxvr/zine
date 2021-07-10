using System;

namespace Argon.Catalog.Application.Reponses
{
    public record TagResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
