using Argon.Catalog.Domain;
using System;
using System.Threading.Tasks;

namespace Argon.Catalog.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public ISupplierRepository SupplierRepository => throw new NotImplementedException();

        public IServiceRepository ServiceRepository => throw new NotImplementedException();

        public Task<bool> CommitAsync()
        {
            throw new NotImplementedException();
        }
    }
}
