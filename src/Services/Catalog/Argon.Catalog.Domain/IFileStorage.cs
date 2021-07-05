using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Domain
{
    public interface IFileStorage
    {
        Task<IEnumerable<Image>?> AddAsync(
            List<(Stream Image, string FileName)> images, CancellationToken cancellationToken = default);
    }
}
