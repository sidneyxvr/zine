using Argon.Core.DomainObjects;
using System;

namespace Argon.Catalog.Domain
{
    public class FeeHomeAssistance : Entity<int>
    {
        public decimal Price { get; private set; }
        public double Radius { get; private set; }

        public Guid ServiceId { get; private set; }
        public Service? Service { get; private set; }

        protected FeeHomeAssistance() { }

        public FeeHomeAssistance(decimal price, double radius)
        {
            Check.Range(price, decimal.Zero, decimal.MaxValue, nameof(price));
            Check.Range(radius, 0.0, double.MaxValue, nameof(radius));

            Price = price;
            Radius = radius;
        }
    }
}
