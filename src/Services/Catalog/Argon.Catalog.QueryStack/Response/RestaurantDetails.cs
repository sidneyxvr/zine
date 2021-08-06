﻿using System;

namespace Argon.Catalog.QueryStack.Response
{
    public record RestaurantDetails
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
        public string? LogoUrl { get; set; }
    }
}
