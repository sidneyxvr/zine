using Argon.Zine.Core.DomainObjects;
using System;

namespace Argon.Zine.Core.Data
{
    public interface IRepository<TEntity> : IDisposable where TEntity : IAggregateRoot
    {
    }
}
