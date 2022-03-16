using Argon.Zine.Commom.Data;
using System.IO;
using System.Threading;

namespace Argon.Zine.App.Api.Tests.FakeServices;

public class FakeFileStorage : IFileStorage
{
    public Task<(string FileName, string Url)> UploadAsync(Stream imageStream, string fileName, CancellationToken cancellationToken = default)
        => Task.FromResult(($"filename{Random.Shared.Next()}", $"url{Random.Shared.Next()}.com"));
}