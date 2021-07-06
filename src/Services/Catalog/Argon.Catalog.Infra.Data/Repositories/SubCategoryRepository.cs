using Argon.Catalog.Domain;
using Argon.Core.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Infra.Data.Repositories
{
    public class SubCategoryRepository : ISubCategoryRepository, IRepository<SubCategory>
    {
        public Task AddAsync(SubCategory subCategory, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}
