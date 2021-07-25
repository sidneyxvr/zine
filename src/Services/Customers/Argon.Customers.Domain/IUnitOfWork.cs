using System.Threading;
using System.Threading.Tasks;

namespace Argon.Customers.Domain
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();

        public ICustomerRepository CustomerRepository { get; }
    }
}
