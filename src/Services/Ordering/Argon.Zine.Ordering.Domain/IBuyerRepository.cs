namespace Argon.Zine.Ordering.Domain;

public interface IBuyerRepository : IDisposable
{
    Task AddAsync(Buyer buyer, CancellationToken cancellationToken);
    Task UpdateAsync(Buyer buyer, CancellationToken cancellationToken);
    Task<Buyer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}