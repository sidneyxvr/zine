using System.Threading.Tasks;

namespace Argon.Suppliers.Domain
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }
}
