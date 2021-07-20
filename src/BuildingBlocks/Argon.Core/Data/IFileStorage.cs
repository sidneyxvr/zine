using Argon.Core.DomainObjects;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Core.Data
{
    public interface IFileStorage
    {
        Task<Image> AddAsync(
            Stream ImageStream, string FileName, 
            CancellationToken cancellationToken = default);
    }
}
