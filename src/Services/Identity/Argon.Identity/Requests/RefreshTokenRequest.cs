namespace Argon.Identity.Requests
{
    public record RefreshTokenRequest : BaseRequest
    {
        public string? AccessToken { get; init; }
        public string? RefreshToken { get; init; }
    }
}
