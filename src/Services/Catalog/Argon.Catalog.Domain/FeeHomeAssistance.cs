using Argon.Core.DomainObjects;
using System.Collections.Generic;

namespace Argon.Catalog.Domain
{
    public class FeeHomeAssistance : ValueObject
    {
        public decimal Price { get; private set; }
        public double Radius { get; private set; }

        protected FeeHomeAssistance() { }

        public FeeHomeAssistance(decimal price, double radius)
        {
            Check.Range(price, decimal.Zero, decimal.MaxValue, nameof(price));
            Check.Range(radius, 0.0, double.MaxValue, nameof(radius));

            Price = price;
            Radius = radius;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Price;
            yield return Radius;
        }
    }
}
