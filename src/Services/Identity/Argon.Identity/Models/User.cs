using Microsoft.AspNetCore.Identity;

namespace Argon.Identity.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
