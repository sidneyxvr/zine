using Argon.Zine.Ordering.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Zine.Ordering.Infra.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderingContext _context;

        public OrderRepository(OrderingContext context)
            => _context = context;

        public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
            => await _context.Orders.AddAsync(order, cancellationToken);

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
