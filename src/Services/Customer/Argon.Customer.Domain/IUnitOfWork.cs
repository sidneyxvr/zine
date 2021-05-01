using System.Threading.Tasks;

namespace Argon.Customers.Domain
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }
}
