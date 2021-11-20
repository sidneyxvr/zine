namespace Argon.Zine.Ordering.Application.Requests;

public class SubmitOrderRequest
{
    public Guid AddressId { get; init; }
    public Guid PaymentMethodId { get; init; }
}