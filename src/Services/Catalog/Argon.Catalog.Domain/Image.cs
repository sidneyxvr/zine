using Argon.Core.DomainObjects;
using System.Collections.Generic;

namespace Argon.Catalog.Domain
{
    public class Image : ValueObject
    {
        public const int UrlMaxLength = 255;

        public string Url { get; private set; }
        public int Order { get; private set; }

        protected Image() { }

        public Image(string url, int order)
        {
            Url = url;
            Order = order;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Url;
            yield return Order;
        }
    }
}
