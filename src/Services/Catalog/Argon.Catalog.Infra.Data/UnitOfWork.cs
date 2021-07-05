using Argon.Catalog.Domain;
using System;
using System.Threading.Tasks;

namespace Argon.Catalog.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public ISupplierRepository SupplierRepository => throw new NotImplementedException();

        public IServiceRepository ServiceRepository => throw new NotImplementedException();

        public ICategoryRepository CategoryRepository => throw new NotImplementedException();

        public IDepartmentRepository DepartmentRepository => throw new NotImplementedException();

        public ISubCategoryRepository SubCategoryRepository => throw new NotImplementedException();

        public ITagRepository TagRepository => throw new NotImplementedException();

        public Task<bool> CommitAsync()
        {
            throw new NotImplementedException();
        }
    }
}
