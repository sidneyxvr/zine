namespace Argon.Zine.Identity.Requests;

public record EmailRequest : BaseRequest
{
    public string? Email { get; set; }
}