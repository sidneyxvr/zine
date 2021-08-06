using Argon.Ordering.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Ordering.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderingContext _context;
        private readonly ILogger<UnitOfWork> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IBuyerRepository _buyerRepository;
        public UnitOfWork(
            OrderingContext context,
            ILogger<UnitOfWork> logger,
            IOrderRepository orderRepository,
            IBuyerRepository buyerRepository)
        {
            _logger = logger;
            _context = context;
            _orderRepository = orderRepository;
            _buyerRepository = buyerRepository;
        }

        public IOrderRepository OrderRepository => _orderRepository;

        public IBuyerRepository BuyerRepository => _buyerRepository;

        public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
        {
            var orders = _context.ChangeTracker.Entries<Order>()
                .Where(e => e.State == EntityState.Added)
                .Select(e => e.Entity)
                .ToList();

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            foreach (var order in orders)
            {
                var currentOrderStatus = order.OrderStatuses.LastOrDefault();
                typeof(Order).GetProperty(nameof(Order.CurrentOrderStatus))!.SetValue(order, currentOrderStatus);
                _context.Entry(order).DetectChanges();
            }

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                orders.ForEach(order =>
                {
                    _logger.LogError($"Cannot add current status to order - Id: {order.Id} - on save changes" +
                        $"Ordering context {nameof(UnitOfWork)}" +
                        $"Assembly {typeof(UnitOfWork).Assembly}",
                        order);
                });
            }

            //TODO: Publish events

            return success;
        }
    }
}
