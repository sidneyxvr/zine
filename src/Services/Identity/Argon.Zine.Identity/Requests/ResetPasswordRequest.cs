namespace Argon.Zine.Identity.Requests
{
    public record ResetPasswordRequest : BaseRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
    }
}
