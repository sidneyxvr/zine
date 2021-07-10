﻿using Argon.Catalog.Domain;
using Argon.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Infra.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository, IRepository<Category>
    {
        private readonly IDbContextFactory<CatalogContext> dbContextFactory;
        public Task AddAsync(Category category, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
