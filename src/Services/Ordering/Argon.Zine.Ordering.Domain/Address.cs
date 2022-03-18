using Argon.Zine.Commom.DomainObjects;
namespace Argon.Zine.Ordering.Domain;

public class Address : ValueObject
{
    public string Street { get; private set; }
    public string? Number { get; private set; }
    public string District { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string PostalCode { get; private set; }
    public string? Complement { get; private set; }

#pragma warning disable CS8618
    private Address() { }
#pragma warning restore CS8618

    public Address(string street, string? number, string district, string city,
        string state, string country, string postalCode, string? complement)
    {
        Street = street;
        Number = number;
        District = district;
        City = city;
        State = state;
        Country = country;
        PostalCode = postalCode;
        Complement = complement;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Street;
        yield return Number;
        yield return District;
        yield return City;
        yield return State;
        yield return Country;
        yield return PostalCode;
        yield return Complement;
    }
}