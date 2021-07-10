using Argon.Catalog.Application.Reponses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Argon.Catalog.Application.Queries
{
    public interface IDepartmentQueries : IDisposable
    {
        Task<IEnumerable<DepartmentResponse>> GetAllAsync();
    }
}
