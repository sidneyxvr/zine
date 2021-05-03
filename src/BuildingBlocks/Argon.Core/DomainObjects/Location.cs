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
            if (latitude is null || longitude is null)
            {
                Check.NotNull(latitude, nameof(latitude));
                Check.NotNull(longitude, nameof(longitude));
            }

            Check.Range(latitude.Value, -90, 90, nameof(latitude));
            Check.Range(longitude.Value, -180, 180, nameof(longitude));

            _coordinate = new Point(latitude.Value, longitude.Value) { SRID = 4326 };
        }

        public double GetDistance(Location location) => 
            _coordinate.Distance(new Point(location.Latitude, location.Longitude) { SRID = 4326 });

        public double GetDistance(double latitude, double longitude) =>
            _coordinate.Distance(new Point(latitude, longitude) { SRID = 4326 });

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Latitude;
            yield return Longitude;
        }
    }
}
