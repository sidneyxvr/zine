using Argon.Catalog.QueryStack.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Argon.Catalog.QueryStack.Queries
{
    public interface IDepartmentQuery
    {
        Task<IEnumerable<DepartmentResult>> GetAllAsync();
    }
}
