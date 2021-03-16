using Microsoft.AspNetCore.Identity;
using System;

namespace Argon.Identity.Models
{
    public class User : IdentityUser<Guid>
    {
        public bool IsActive { get; set; }
    }
}
