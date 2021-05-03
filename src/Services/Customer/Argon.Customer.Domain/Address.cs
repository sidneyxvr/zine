using Argon.Core.DomainObjects;

namespace Argon.Customers.Domain
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
            Check.NotEmpty(Street, nameof(Street));
            Check.Range(Street, 2, 50, nameof(Street));

            Check.Range(Number, 1, 5, nameof(Number));

            Check.Range(Complement, 2, 50, nameof(Complement));

            Check.NotEmpty(District, nameof(District));
            Check.Range(District, 2, 50, nameof(District));

            Check.NotEmpty(City, nameof(City));
            Check.Range(City, 2, 40, nameof(City));

            Check.NotEmpty(State, nameof(State));
            Check.Length(State, 2, nameof(State));

            Check.NotEmpty(PostalCode, nameof(PostalCode));
            Check.Length(PostalCode, 8, nameof(PostalCode));

            Check.NotEmpty(Country, nameof(Country));
            Check.Range(Country, 2, 50, nameof(Country));
        }

        public override string ToString()
        {
            return $"{Street}, {Number} - {District}, {City} - {State}";
        }
    }
}
