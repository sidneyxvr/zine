using Microsoft.AspNetCore.Identity;

namespace Argon.Zine.Identity.Models;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public bool IsActive { get; set; }
}