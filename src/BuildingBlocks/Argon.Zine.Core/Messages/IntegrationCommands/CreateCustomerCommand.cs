using Argon.Zine.Core.Utils;

namespace Argon.Zine.Core.Messages.IntegrationCommands;

public record CreateCustomerCommand : Command
{
    public Guid UserId { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }
    public string? Phone { get; init; }
    private string? _cpf;
    public string? Cpf { get => _cpf?.OnlyNumbers(); init => _cpf = value; }
    public DateTime BirthDate { get; init; }
    public string? ConfirmationToken { get; init; }
}