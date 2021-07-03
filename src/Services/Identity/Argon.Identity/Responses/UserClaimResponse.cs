namespace Argon.Identity.Responses
{
    public record UserClaimResponse
    {
        public string Value { get; init; } = null!;
        public string Type { get; set; } = null!;
    }
}