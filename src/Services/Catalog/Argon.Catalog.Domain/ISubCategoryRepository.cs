using Argon.Core.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Domain
{
    public interface ISubCategoryRepository : IRepository<SubCategory>
    {
        Task AddAsync(SubCategory subCategory, CancellationToken cancellationToken = default);
    }
}
