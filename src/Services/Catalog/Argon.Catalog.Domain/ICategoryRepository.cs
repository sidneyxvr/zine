﻿using Argon.Core.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Domain
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
        Task AddAsync(Category category, CancellationToken cancellationToken = default);
        Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
