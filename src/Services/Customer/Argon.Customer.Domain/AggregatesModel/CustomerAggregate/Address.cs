using Argon.Core.DomainObjects;
using NetTopologySuite.Geometries;

namespace Argon.Customers.Domain.AggregatesModel.CustomerAggregate
{
    public class Address : Entity
    {
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string District { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string PostalCode { get; private set; }
        public string Complement { get; private set; }

        private readonly Point _location;
        public double? Latitude => _location?.X;
        public double? Longitude => _location?.Y;
        protected Address() { }

        public Address(string street, string number, string district, string city, string state, 
            string country, string postalCode, string complement, double? latitude, double? longitude)
        {
            ValidateStreet(street);
            ValidateNumber(number);
            ValidateDistrict(district);
            ValidateCity(city);
            ValidateState(state);
            ValidateCountry(country);
            ValidatePostalCode(postalCode);
            ValidateComplement(complement);
            ValidateCoordinates(latitude, longitude);

            Street = street;
            Number = number;
            District = district;
            City = city;
            State = state;
            Country = country;
            PostalCode = postalCode;
            Complement = complement;
            _location = latitude.HasValue && longitude.HasValue ? new Point(latitude.Value, longitude.Value) : null;
        }

        private void ValidateStreet(string street)
        {
            AssertionConcern.AssertArgumentNotEmpty(street, Localizer.GetTranslation("EmptyStreet"));
            AssertionConcern.AssertArgumentRange(street, 2, 50, Localizer.GetTranslation("StreetOutOfRange"));
        } 

        private void ValidateNumber(string number) =>
            AssertionConcern.AssertArgumentRange(number, 1, 5, Localizer.GetTranslation("NumberMaxLength"));

        private void ValidateComplement(string complement) =>
            AssertionConcern.AssertArgumentRange(complement, 2, 50, Localizer.GetTranslation("ComplementMaxLength"));

        private void ValidateDistrict(string district)
        {
            AssertionConcern.AssertArgumentNotEmpty(district, Localizer.GetTranslation("EmptyDistrict"));
            AssertionConcern.AssertArgumentRange(district, 2, 50, Localizer.GetTranslation("DistrictOutOfRange"));
        }

        private void ValidateCity(string city)
        {
            AssertionConcern.AssertArgumentNotEmpty(city, Localizer.GetTranslation("EmptyCity"));
            AssertionConcern.AssertArgumentRange(city, 2, 40, Localizer.GetTranslation("CityOutOfRange"));
        }

        private void ValidateState(string state)
        {
            AssertionConcern.AssertArgumentNotEmpty(state, Localizer.GetTranslation("EmptyState"));
            AssertionConcern.AssertArgumentExactLength(state, 2, Localizer.GetTranslation("InvalidState"));
        }

        private void ValidatePostalCode(string postalCode)
        {
            AssertionConcern.AssertArgumentNotEmpty(postalCode, Localizer.GetTranslation("EmptyPostalCode"));
            AssertionConcern.AssertArgumentExactLength(postalCode, 8, Localizer.GetTranslation("InvalidPostalCode"));
        }

        private void ValidateCountry(string country)
        {
            AssertionConcern.AssertArgumentNotEmpty(country, Localizer.GetTranslation("EmptyCountry"));
            AssertionConcern.AssertArgumentRange(country, 2, 50, Localizer.GetTranslation("CountryOutOfRange"));
        }

        private void ValidateCoordinates(double? latitude, double? longitude)
        {
            if(latitude is null && longitude is null)
            {
                return;
            }

            if((latitude is null && longitude is not null) || (latitude is not null && longitude is null))
            {
                throw new DomainException(Localizer.GetTranslation("InvalidCoordinates"));
            }

            AssertionConcern.AssertArgumentRange(latitude.Value, -90, 90, Localizer.GetTranslation("InvalidLatitude"));
            AssertionConcern.AssertArgumentRange(longitude.Value, -180, 180, Localizer.GetTranslation("InvalidLongitude"));
        }
    }
}
