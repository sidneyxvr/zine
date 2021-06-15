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
