using Argon.Identity.Data;
using Argon.Identity.Models;
using System;

namespace Argon.WebApp.Tests.Configuration
{
    public static class SeederIdentityContext
    {
        public static IdentityContext Seed(this IdentityContext context)
        {
            context.Roles.AddRange(
                new() { Id = new ("3215CA3D-EC71-4DF4-BF41-555FFCC04F22"), Name = "Customer", NormalizedName = "CUSTOMER", ConcurrencyStamp = "a76ddbb4-c77f-410f-83cd-9d66e3dc893e" },
                new() { Id = new ("07E778B6-7C1D-4852-B1F4-63661E4BB08A"), Name = "Supplier", NormalizedName = "SUPPLIER", ConcurrencyStamp = "11a13445-7bba-4fad-86b1-ac3cef32e569" },
                new() { Id = new ("78DF47E2-8160-4678-99B0-2E80492268DD"), Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "bc6e8e27-8e7f-413d-bd2d-982b4d80a801" });

            var email = "teste@email.com";
            context.Users.Add(new ()
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
            });

            context.UserRoles.Add(new() { RoleId = new("3215CA3D-EC71-4DF4-BF41-555FFCC04F22"), UserId = new("0D75B59A-EEF9-45A1-B08A-4F4878DC33B8") });

            context.SaveChanges();

            return context;
        }
    }
}
