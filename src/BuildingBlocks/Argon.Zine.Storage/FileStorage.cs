using Argon.Zine.Core.Data;
using Argon.Zine.Core.DomainObjects;

namespace Argon.Storage
{
    public class FileStorage : IFileStorage
    {
        public Task<Image> AddAsync(
            Stream ImageStream, string FileName,
            CancellationToken cancellationToken = default)
                => Task.FromResult(new Image("imageUrl", "fileName"));
    }
}
