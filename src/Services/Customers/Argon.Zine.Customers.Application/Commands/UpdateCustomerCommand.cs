using Argon.Zine.Commom.Messages;

namespace Argon.Zine.Customers.Application.Commands;

public record UpdateCustomerCommand : Command
{
    public string? FirstName { get; init; }
    public string? Surname { get; init; }
    public string? Phone { get; init; }
    public DateTime BirthDate { get; init; }
}