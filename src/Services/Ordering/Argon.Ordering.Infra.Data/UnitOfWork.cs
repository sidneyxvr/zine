using Argon.Ordering.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Ordering.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderingContext _context;
        private readonly IOrderRepository _orderRepository;
        private readonly IBuyerRepository _buyerRepository;
        public UnitOfWork(
            OrderingContext context,
            IOrderRepository orderRepository,
            IBuyerRepository buyerRepository)
        {
            _context = context;
            _orderRepository = orderRepository;
            _buyerRepository = buyerRepository;
        }

        public IOrderRepository OrderRepository => _orderRepository;

        public IBuyerRepository BuyerRepository => _buyerRepository;

        public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
        {
            var success = await _context.SaveChangesAsync(cancellationToken);

            //TODO: Publish events

            return success > 0;
        }
    }
}
