using NetTopologySuite.Geometries;
using System.Collections.Generic;

namespace Argon.Core.DomainObjects
{
    public class Location : ValueObject
    {
        private readonly Point _coordinate;

        public double Latitude => _coordinate.X;
        public double Longitude => _coordinate.Y;

        protected Location() { }

        public Location(double? latitude, double? longitude)
        {
            if ((latitude is null && longitude is not null) || (latitude is not null && longitude is null))
            {
                throw new DomainException(Localizer.GetTranslation("InvalidCoordinates"));
            }

            AssertionConcern.AssertArgumentRange(latitude.Value, -90, 90, Localizer.GetTranslation("InvalidLatitude"));
            AssertionConcern.AssertArgumentRange(longitude.Value, -180, 180, Localizer.GetTranslation("InvalidLongitude"));

            _coordinate = new Point(latitude.Value, longitude.Value);
        }

        public double GetDistance(Location location) => 
            _coordinate.Distance(new Point(location.Latitude, location.Longitude));

        public double GetDistance(double latitude, double longitude) =>
            _coordinate.Distance(new Point(latitude, longitude));

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Latitude;
            yield return Longitude;
        }
    }
}
