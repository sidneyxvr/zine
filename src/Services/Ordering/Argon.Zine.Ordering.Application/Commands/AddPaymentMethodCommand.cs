using Argon.Zine.Core.Messages;

namespace Argon.Zine.Ordering.Application.Commands;

public record AddPaymentMethodCommand : Command
{
    public Guid CustomerId { get; set; }
    public string CustomerFirstName { get; set; } = null!;
    public string CustomerLastName { get; set; } = null!;
    public string Alias { get; init; } = null!;
    public string CardNamber { get; init; } = null!;
    public string CardHolderName { get; init; } = null!;
    public DateTime Expiration { get; init; }
}