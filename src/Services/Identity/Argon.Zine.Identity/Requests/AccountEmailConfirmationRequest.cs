namespace Argon.Zine.Identity.Requests;

public record EmailAccountConfirmationRequest : BaseRequest
{
    public string? Email { get; init; }
    public string? Token { get; init; }
}