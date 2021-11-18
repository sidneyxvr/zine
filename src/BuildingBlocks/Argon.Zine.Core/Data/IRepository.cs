using Argon.Zine.Core.DomainObjects;

namespace Argon.Zine.Core.Data;

public interface IRepository<TEntity> : IDisposable where TEntity : IAggregateRoot
{
}