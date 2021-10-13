using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Zine.Core.Data
{
    public interface IFileStorage
    {
        Task<(string FileName, string Url)> UploadAsync(
            Stream imageStream, string fileName, 
            CancellationToken cancellationToken = default);
    }
}
