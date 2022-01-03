namespace Argon.Zine.Storage;

#pragma warning disable CS8618
public class S3Settings
{
    public string AccessId { get; set; }
    public string AccessKey {  get; set; }
    public string BucketName { get; set; }
    public string Region { get; set; }
}
#pragma warning restore CS8618