using Argon.Catalog.Domain;
using System;
using System.Threading.Tasks;

namespace Argon.Catalog.Infra.Data.Repositories
{
    public class DepartmentRepository 
        //: IDepartmentRepository
    {
        private readonly CatalogContext _context;

        public DepartmentRepository(CatalogContext context)
        {
            _context = context;
        }

        public Task AddAsync(Department department)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
