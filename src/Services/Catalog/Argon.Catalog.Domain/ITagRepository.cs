using Argon.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Argon.Catalog.Domain
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<List<Tag>> GetByIdsAsync(IEnumerable<int> ids);
    }
}
