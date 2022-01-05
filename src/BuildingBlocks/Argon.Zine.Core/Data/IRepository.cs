using Argon.Zine.Commom.DomainObjects;

namespace Argon.Zine.Commom.Data;

public interface IRepository<TEntity> : IDisposable where TEntity : IAggregateRoot
{
}