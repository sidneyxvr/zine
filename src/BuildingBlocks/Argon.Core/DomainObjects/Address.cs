using NetTopologySuite.Geometries;
using System;

namespace Argon.Core.DomainObjects
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
        public Location Location { get; private set; }
        protected Address() { }

        public Address(string street, string number, string district, string city, string state,
            string country, string postalCode, string complement, double? latitude, double? longitude)
        {
            Street = street;
            Number = number;
            District = district;
            City = city;
            State = state;
            Country = country;
            PostalCode = postalCode;
            Complement = complement;
            Location = latitude.HasValue && longitude.HasValue ? new Location(latitude.Value, longitude.Value) : null;

            Validate();
        }

        public void Update(string street, string number, string district, string city, string state,
            string country, string postalCode, string complement, double? latitude, double? longitude)
        {
            Street = street;
            Number = number;
            District = district;
            City = city;
            State = state;
            Country = country;
            PostalCode = postalCode;
            Complement = complement;
            Location = latitude.HasValue && longitude.HasValue ? new Location(latitude.Value, longitude.Value) : null;

            Validate();
        }

        private void Validate()
        {
            AssertionConcern.AssertArgumentNotEmpty(Street, Localizer.GetTranslation("EmptyStreet"));
            AssertionConcern.AssertArgumentRange(Street, 2, 50, Localizer.GetTranslation("StreetOutOfRange"));

            AssertionConcern.AssertArgumentRange(Number, 1, 5, Localizer.GetTranslation("NumberMaxLength"));

            AssertionConcern.AssertArgumentRange(Complement, 2, 50, Localizer.GetTranslation("ComplementMaxLength"));

            AssertionConcern.AssertArgumentNotEmpty(District, Localizer.GetTranslation("EmptyDistrict"));
            AssertionConcern.AssertArgumentRange(District, 2, 50, Localizer.GetTranslation("DistrictOutOfRange"));

            AssertionConcern.AssertArgumentNotEmpty(City, Localizer.GetTranslation("EmptyCity"));
            AssertionConcern.AssertArgumentRange(City, 2, 40, Localizer.GetTranslation("CityOutOfRange"));

            AssertionConcern.AssertArgumentNotEmpty(State, Localizer.GetTranslation("EmptyState"));
            AssertionConcern.AssertArgumentExactLength(State, 2, Localizer.GetTranslation("InvalidState"));

            AssertionConcern.AssertArgumentNotEmpty(PostalCode, Localizer.GetTranslation("EmptyPostalCode"));
            AssertionConcern.AssertArgumentExactLength(PostalCode, 8, Localizer.GetTranslation("InvalidPostalCode"));

            AssertionConcern.AssertArgumentNotEmpty(Country, Localizer.GetTranslation("EmptyCountry"));
            AssertionConcern.AssertArgumentRange(Country, 2, 50, Localizer.GetTranslation("CountryOutOfRange"));
        }

        public override string ToString()
        {
            return $"{Street}, {Number} - {District}, {City} - {State}";
        }
    }
}
