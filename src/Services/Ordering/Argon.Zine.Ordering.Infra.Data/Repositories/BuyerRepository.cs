using Argon.Zine.Ordering.Domain;
using Microsoft.EntityFrameworkCore;

namespace Argon.Zine.Ordering.Infra.Data.Repositories;

public class BuyerRepository : IBuyerRepository
{
    private readonly OrderingContext _context;

    public BuyerRepository(OrderingContext context)
        => _context = context;

    public async Task AddAsync(Buyer buyer, CancellationToken cancellationToken)
        => await _context.Buyers.AddAsync(buyer, cancellationToken);

    public void Dispose()
    {
        _context?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<Buyer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _context.Buyers.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

    public Task UpdateAsync(Buyer buyer, CancellationToken cancellationToken)
        => Task.CompletedTask;
}