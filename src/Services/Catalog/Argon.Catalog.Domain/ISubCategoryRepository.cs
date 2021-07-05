using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Domain
{
    public interface ISubCategoryRepository
    {
        Task AddAsync(SubCategory subCategory, CancellationToken cancellationToken = default);
    }
}
