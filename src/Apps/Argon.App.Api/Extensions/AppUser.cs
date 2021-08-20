using Argon.Core.DomainObjects;
using System.IdentityModel.Tokens.Jwt;

namespace Argon.App.Api.Extensions
{
    public class AppUser : IAppUser
    {
        private readonly IHttpContextAccessor? _accessor;

        public AppUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));

            if (_accessor.HttpContext?.User.Identity!.IsAuthenticated == false)
            {
                throw new InvalidOperationException("Cannot get authenticated user");
            }

            var claims = _accessor.HttpContext!.User.Claims;

            Id = new(claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)!.Value);
            FirstName = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.GivenName)!.Value;
            LastName = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.FamilyName)!.Value;
        }

        public Guid Id { get; init; } 
        public string FirstName { get; init; } 
        public string LastName { get; init; } 

        public string FullName => $"{FirstName} {LastName}";
    }
}
