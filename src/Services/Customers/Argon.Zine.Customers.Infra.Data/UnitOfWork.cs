using Argon.Zine.Commom.Communication;
using Argon.Zine.Customers.Domain;

namespace Argon.Zine.Customers.Infra.Data;

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

        if (success) await _bus.PublishEventsAsync(_context);

        return success;
    }
}