namespace Argon.Zine.Ordering.Domain;

public interface IOrderRepository : IDisposable
{
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
}