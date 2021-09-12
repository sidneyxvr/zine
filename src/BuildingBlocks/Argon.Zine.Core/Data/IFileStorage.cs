using Argon.Zine.Core.DomainObjects;

namespace Argon.Zine.Core.Data
{
    public interface IFileStorage
    {
        Task<Image> AddAsync(
            Stream ImageStream, string FileName, 
            CancellationToken cancellationToken = default);
    }
}
