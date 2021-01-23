using System.Threading.Tasks;

namespace Argon.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }
}
