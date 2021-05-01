using Argon.Core.Communication;
using Argon.Customers.Domain;
using System;
using System.Threading.Tasks;

namespace Argon.Customers.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IBus _bus;
        private readonly CustomerContext _context;
        
        public UnitOfWork(IBus bus, CustomerContext context)
        {
            _bus = bus;
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            var success = await _context.SaveChangesAsync() > 0;

            if (success) await _bus.PublicarEventos(_context);

            return success;
        }
    }
}
