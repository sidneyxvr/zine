namespace Argon.Zine.Commom.Data;

public interface IFileStorage
{
    Task<(string FileName, string Url)> UploadAsync(
        Stream imageStream, string fileName,
        CancellationToken cancellationToken = default);
}