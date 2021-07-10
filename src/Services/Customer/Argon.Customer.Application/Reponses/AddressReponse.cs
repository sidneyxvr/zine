namespace Argon.Customers.Application.Reponses
{
    public class AddressReponse
    {
        public string? Street { get; init; }
        public string? Number { get; init; }
        public string? District { get; init; }
        public string? City { get; init; }
        public string? State { get; init; }
        public string? Country { get; init; }
        public string? PostalCode { get; init; }
        public string? Complement { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
    }
}
