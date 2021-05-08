using Argon.Core.Communication;
using Argon.Suppliers.Domain;
using System.Threading.Tasks;

namespace Argon.Suppliers.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IBus _bus;
        private readonly SupplierContext _context;
        private readonly ISupplierRepository _supplierRepository;

        public UnitOfWork(IBus bus, 
            SupplierContext context, 
            ISupplierRepository supplierRepository)
        {
            _bus = bus;
            _context = context;
            _supplierRepository = supplierRepository;
        }

        public ISupplierRepository SupplierRepository => _supplierRepository;

        public async Task<bool> CommitAsync()
        {
            var success = await _context.SaveChangesAsync() > 0;

            if (success) await _bus.PublicarEventos(_context);

            return true;
        }
    }
}
