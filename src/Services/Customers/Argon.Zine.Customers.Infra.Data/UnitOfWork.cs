using Argon.Zine.Core.Communication;
using Argon.Zine.Customers.Domain;
using System.Threading.Tasks;

namespace Argon.Zine.Customers.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IBus _bus;
        private readonly CustomerContext _context;
        private readonly ICustomerRepository _customerRepository;

        public UnitOfWork(IBus bus, CustomerContext context, ICustomerRepository customerRepository)
        {
            _bus = bus;
            _context = context;
            _customerRepository = customerRepository;
        }

        public ICustomerRepository CustomerRepository => _customerRepository;

        public async Task<bool> CommitAsync()
        {
            var success = await _context.SaveChangesAsync() > 0;

            if (success) await _bus.PublicarEventos(_context);

            return success;
        }
    }
}
