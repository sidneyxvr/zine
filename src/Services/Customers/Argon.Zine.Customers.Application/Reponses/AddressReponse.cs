namespace Argon.Zine.Customers.Application.Reponses;

public record AddressReponse (
    string Street, string? Number, 
    string District, string City, 
    string State, string Country,
    string PostalCode, string? Complement,
    double Latitude, double Longitude);