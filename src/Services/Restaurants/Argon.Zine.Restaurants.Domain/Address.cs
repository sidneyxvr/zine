using Argon.Zine.Commom.DomainObjects;

namespace Argon.Restaurants.Domain;

public class Address : Entity
{
    public string Street { get; private set; }
    public string Number { get; private set; }
    public string District { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string PostalCode { get; private set; }
    public string? Complement { get; private set; }
    public Location Location { get; private set; }

    public Guid? RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }

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

#pragma warning disable CS8618
    protected Address() { }
#pragma warning restore CS8618

    public Address(string street, string number, string district, 
        string city, string state, string postalCode, Location location)
    {
        Validate(street, number, district, city, state, postalCode);

        Street = street;
        Number = number;
        District = district;
        City = city;
        State = state;
        Country = "Brasil";
        PostalCode = postalCode;
        Location = location;
    }

    public void Update(string street, string number, string district, 
        string city, string state, string postalCode, Location location)
    {
        Validate(street, number, district, city, state, postalCode);

        Street = street;
        Number = number;
        District = district;
        City = city;
        State = state;
        PostalCode = postalCode;
        Location = location;
    }

    public void SetComplement(string complement)
    {
        Check.MaxLength(complement, ComplementMaxLength, nameof(complement));

        Complement = complement;
    }

    private static void Validate(string street, string number, 
        string district, string city, string state, string postalCode)
    {
        Check.NotEmpty(street, nameof(street));
        Check.Range(street!, StreetMinLength, StreetMaxLength, nameof(street));

        Check.MaxLength(number, NumberMaxLength, nameof(number));

        Check.NotEmpty(district, nameof(district));
        Check.Range(district!, DistrictMinLength, DistrictMaxLength, nameof(district));

        Check.NotEmpty(city, nameof(city));
        Check.Range(city!, CityMinLength, CityMaxLength, nameof(city));

        Check.NotEmpty(state, nameof(state));
        Check.Length(state!, StateLength, nameof(state));

        Check.NotEmpty(postalCode, nameof(postalCode));
        Check.Length(postalCode!, PostalCodeLength, nameof(postalCode));
    }

    public override string ToString()
    {
        return $"{Street}, {Number} - {District}, {City} - {State}";
    }
}