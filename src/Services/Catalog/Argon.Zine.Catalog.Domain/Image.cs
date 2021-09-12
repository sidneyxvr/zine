using Argon.Zine.Core.DomainObjects;
using System;

namespace Argon.Zine.Catalog.Domain
{
    public class Image : Entity
    {
        public const int UrlMaxLength = 255;

        public string Url { get; private set; }
        public int Order { get; private set; }

        public Guid ServiceId { get; private set; }
        public Product? Service { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Image() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Image(string url, int order)
        {
            Url = url;
            Order = order;
        }
    }
}
