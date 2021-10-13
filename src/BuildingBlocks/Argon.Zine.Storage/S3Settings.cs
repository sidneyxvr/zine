namespace Argon.Zine.Storage;

public class S3Settings
{
    public string AccessId { get; set; } = null!;
    public string AccessKey {  get; set; } = null!;
    public string BucketName { get; set; } = null!;
    public string Region { get; set; } = null!;
}