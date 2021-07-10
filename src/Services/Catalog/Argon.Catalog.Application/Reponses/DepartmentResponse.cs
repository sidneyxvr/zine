﻿using System;

namespace Argon.Catalog.Application.Reponses
{
    public record DepartmentResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
        public string? Description { get; init; }
    }
}
