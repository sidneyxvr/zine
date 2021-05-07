using Argon.Core.DomainObjects;

namespace Argon.Suppliers.Domain
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

        public const int StreetMaxLength = 50;
        public const int StreetMinLength = 2;
        public const int NumberMaxLength = 10;
        public const int NumberMinLength = 1;
        public const int DistrictMaxLength = 50;
        public const int DistrictMinLength = 5;
        public const int CityMaxLength = 40;
        public const int CityMinLength = 2;
        public const int StateLength = 2;
        public const int PostalCodeLength = 8;
        public const int ComplementMaxLength = 50;

        protected Address() { }

        public Address(string street, string number, string district, string city, string state,
            string postalCode, string complement, double? latitude, double? longitude)
        {
            Street = street;
            Number = number;
            District = district;
            City = city;
            State = state;
            Country = "Brasil";
            PostalCode = postalCode;
            Complement = complement;
            Location = latitude.HasValue && longitude.HasValue ? new Location(latitude.Value, longitude.Value) : null;

            Validate();
        }

        public void Update(string street, string number, string district, string city, 
            string state, string postalCode, string complement, double? latitude, double? longitude)
        {
            Street = street;
            Number = number;
            District = district;
            City = city;
            State = state;
            PostalCode = postalCode;
            Complement = complement;
            Location = latitude.HasValue && longitude.HasValue ? new Location(latitude.Value, longitude.Value) : null;

            Validate();
        }

        private void Validate()
        {
            Check.NotEmpty(Street, nameof(Street));
            Check.Range(Street, StreetMinLength, StreetMaxLength, nameof(Street));

            Check.Range(Number, NumberMinLength, NumberMaxLength, nameof(Number));

            Check.MaxLength(Complement, ComplementMaxLength, nameof(Complement));

            Check.NotEmpty(District, nameof(District));
            Check.Range(District, DistrictMinLength, DistrictMaxLength, nameof(District));

            Check.NotEmpty(City, nameof(City));
            Check.Range(City, CityMinLength, CityMaxLength, nameof(City));

            Check.NotEmpty(State, nameof(State));
            Check.Length(State, StateLength, nameof(State));

            Check.NotEmpty(PostalCode, nameof(PostalCode));
            Check.Length(PostalCode, PostalCodeLength, nameof(PostalCode));
        }

        public override string ToString()
        {
            return $"{Street}, {Number} - {District}, {City} - {State}";
        }
    }
}
