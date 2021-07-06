using Argon.Catalog.Domain;
using Argon.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Argon.Catalog.Infra.Data.Repositories
{
    public class TagRepository : ITagRepository, IRepository<Tag>
    {
        public void Dispose()
        {
        }

        public Task<List<Tag>> GetByIdsAsync(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }
    }
}
