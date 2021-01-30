using Argon.Core.DomainObjects;

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
        public Location Location { get; private set; }
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

            Street = street;
            Number = number;
            District = district;
            City = city;
            State = state;
            Country = country;
            PostalCode = postalCode;
            Complement = complement;
            Location = latitude.HasValue && longitude.HasValue ? new Location(latitude.Value, longitude.Value) : null;
        }

        public void Update(string street, string number, string district, string city, string state,
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

            Street = street;
            Number = number;
            District = district;
            City = city;
            State = state;
            Country = country;
            PostalCode = postalCode;
            Complement = complement;
            Location = latitude.HasValue && longitude.HasValue ? new Location(latitude.Value, longitude.Value) : null;
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
    }
}
