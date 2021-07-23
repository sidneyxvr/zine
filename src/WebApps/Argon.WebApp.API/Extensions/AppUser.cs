using Argon.Core.DomainObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace Argon.WebApp.API.Extensions
{
    public class AppUser : IAppUser
    {
        public readonly IHttpContextAccessor _accessor;

        public AppUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));

            if (_accessor.HttpContext!.User.Identity!.IsAuthenticated)
            {
                Id = new Guid(_accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
            }
        }

        public Guid Id { get; set; } = new Guid("D5683FAE-C7C0-4380-81E2-285704723B48");
    }
}
