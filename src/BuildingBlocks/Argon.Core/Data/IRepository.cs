using Argon.Core.DomainObjects;

namespace Argon.Core.Data
{
    public interface IRepository<TEntity> where TEntity : IAggregaeteRoot
    {
    }
}
