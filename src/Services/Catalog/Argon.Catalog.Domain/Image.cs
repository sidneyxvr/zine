using Argon.Core.DomainObjects;
using System.Collections.Generic;

namespace Argon.Catalog.Domain
{
    public class Image : ValueObject
    {
        public const int UrlMaxLength = 255;

        public string Url { get; private set; }
        public int Order { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Image() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

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
