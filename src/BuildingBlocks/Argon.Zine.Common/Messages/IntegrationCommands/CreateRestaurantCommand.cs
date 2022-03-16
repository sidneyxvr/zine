using Microsoft.AspNetCore.Http;

namespace Argon.Zine.Commom.Messages.IntegrationCommands;

#pragma warning disable CS8618 
public record CreateRestaurantCommand : Command
{
    public Guid UserId { get; init; }
    public string? CorparateName { get; init; }
    public string? TradeName { get; init; }
    public string? CpfCnpj { get; init; }
    public string? Email { get; init; }

    //Address
    public string? Street { get; init; }
    public string? Number { get; init; }
    public string? District { get; init; }
    public string? City { get; init; }
    public string? State { get; init; }
    public string? PostalCode { get; init; }
    public string? Complement { get; init; }
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }

    //Logo
    public IFormFile? Logo { get; init; }
}
#pragma warning restore CS8618