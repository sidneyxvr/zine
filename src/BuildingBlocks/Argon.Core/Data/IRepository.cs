using Argon.Core.DomainObjects;
using System;

namespace Argon.Core.Data
{
    public interface IRepository<TEntity> : IDisposable where TEntity : IAggregateRoot
    {
    }
}
