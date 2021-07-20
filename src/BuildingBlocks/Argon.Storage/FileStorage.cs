using Argon.Core.Data;
using Argon.Core.DomainObjects;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

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
