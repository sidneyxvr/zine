namespace Argon.Identity.Requests
{
    public record EmailRequest : BaseRequest
    {
        public string? Email { get; set; }
    }
}
