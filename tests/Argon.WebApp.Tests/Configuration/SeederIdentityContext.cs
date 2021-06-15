using Argon.Identity.Data;
using Argon.Identity.Models;
using System;

namespace Argon.WebApp.Tests.Configuration
{
    public static class SeederIdentityContext
    {
        public static IdentityContext Seed(this IdentityContext context)
        {
            var email = "teste@email.com";
            var user = new User
            {
                Id = new("0D75B59A-EEF9-45A1-B08A-4F4878DC33B8"),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                UserName = email,
                NormalizedUserName = email.ToUpper(),
                EmailConfirmed = true,
                IsActive = true,
                PasswordHash = "AQAAAAEAACcQAAAAEMQak6/g1zoxz8PLhVgizRnY+iPxU6dHvLRTpmpxAi4X4+Rj0UzsZS8VyGKENlOO5A==", //Teste@123
                SecurityStamp = "G6ELXVESVF2E5DU65KY4RJ47BWKZDYQL",
                ConcurrencyStamp = "e4c12d83-0c98-47b1-bcc3-745d16dfc732",
            };

            context.Users.Add(user);

            context.UserRoles.Add(new() { RoleId = new("3215CA3D-EC71-4DF4-BF41-555FFCC04F22"), UserId = user.Id });    

            context.SaveChanges();

            return context;
        }
    }
}
