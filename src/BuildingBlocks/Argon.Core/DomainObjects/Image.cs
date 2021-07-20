namespace Argon.Core.DomainObjects
{
    public class Image
    {
        public string FileName { get; private set; }
        public string ImageUrl { get; private set; }

        public Image(string fileName, string imageUrl)
        {
            FileName = fileName;
            ImageUrl = imageUrl;
        }
    }
}
