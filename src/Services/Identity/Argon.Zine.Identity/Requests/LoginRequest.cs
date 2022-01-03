namespace Argon.Zine.Identity.Requests;

public record LoginRequest : BaseRequest
{
    public string? Email { get; init; }
    public string? Password { get; init; }
}