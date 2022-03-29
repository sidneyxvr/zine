using Argon.Zine.Commom.Utils;

namespace Argon.Zine.Commom.Messages.IntegrationCommands;

public record CreateCustomerCommand : Command
{
    public Guid UserId { get; init; }
    public string? FirstName { get; init; }
    public string? Surname { get; init; }
    public string? Email { get; init; }
    public string? Phone { get; init; }
    private string? _cpf;
    public string? Cpf { get => _cpf?.OnlyNumbers(); init => _cpf = value; }
    public DateTime BirthDate { get; init; }
    public string? ConfirmationToken { get; init; }
}