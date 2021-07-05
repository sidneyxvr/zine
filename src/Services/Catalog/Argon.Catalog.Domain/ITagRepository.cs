using System.Collections.Generic;
using System.Threading.Tasks;

namespace Argon.Catalog.Domain
{
    public interface ITagRepository
    {
        Task<List<Tag>> GetByIdsAsync(IEnumerable<int> ids);
    }
}
