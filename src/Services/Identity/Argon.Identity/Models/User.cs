using Microsoft.AspNetCore.Identity;

namespace Argon.Identity.Models
{
    public class User : IdentityUser<Guid>
    {
        public bool IsActive { get; set; }
    }
}
