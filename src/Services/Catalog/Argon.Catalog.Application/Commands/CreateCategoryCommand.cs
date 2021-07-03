﻿using Argon.Core.Messages;
using System;

namespace Argon.Catalog.Application.Commands
{
    public record CreateCategoryCommand : Command
    {
        public string? Name { get; init; }
        public string? Description { get; init; }
        public Guid DepartmentId { get; init; }
    }
}
