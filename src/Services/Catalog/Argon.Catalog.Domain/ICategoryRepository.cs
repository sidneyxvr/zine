using System;
using System.Threading.Tasks;

namespace Argon.Catalog.Domain
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
        Task<Category> GetByIdAsync(Guid id);
    }
}
