using Argon.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Argon.Identity.Data
{
    public class IdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = Guid.NewGuid(), Name = "Customer", NormalizedName = "CUSTOMER", ConcurrencyStamp = Guid.NewGuid().ToString() },
                new ApplicationRole { Id = Guid.NewGuid(), Name = "Supplier", NormalizedName = "SUPPLIER", ConcurrencyStamp = Guid.NewGuid().ToString() },
                new ApplicationRole { Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = Guid.NewGuid().ToString() });

            builder.Entity<ApplicationUser>(options =>
            {
                options.ToTable("User");

                options.HasKey(user => user.Id);

                options.Property(user => user.UserName).HasColumnType("varchar(256)");
                options.Property(user => user.NormalizedUserName).HasColumnType("varchar(256)");

                options.Property(user => user.Email).HasColumnType("varchar(256)");
                options.Property(user => user.NormalizedEmail).HasColumnType("varchar(256)");

                options.Property(user => user.PhoneNumber).HasColumnType("varchar(20)");

                options.Property(user => user.SecurityStamp).HasColumnType("varchar(256)");
                options.Property(user => user.ConcurrencyStamp).HasColumnType("varchar(256)");
                options.Property(user => user.PasswordHash).HasColumnType("varchar(256)");
            });

            builder.Entity<ApplicationRole>(options =>
            {
                options.ToTable("Role");

                options.HasKey(role => role.Id);

                options.Property(role => role.Name).HasColumnType("varchar(256)");
                options.Property(role => role.NormalizedName).HasColumnType("varchar(256)");

            });

            builder.Entity<IdentityUserRole<Guid>>(options =>
            {
                options.ToTable("UserRole");

                options.HasKey(userRole => new { userRole.UserId, userRole.RoleId });
            });

            builder.Ignore<IdentityRoleClaim<Guid>>();
            builder.Ignore<IdentityUserLogin<Guid>>();
            builder.Ignore<IdentityUserToken<Guid>>();
            builder.Ignore<IdentityUserClaim<Guid>>();

            //builder.Entity<IdentityRoleClaim<Guid>>(options =>
            //{
            //    options.HasKey(roleClaim => roleClaim.Id);

            //    options.Property(roleClaim => roleClaim.ClaimType).HasColumnType("varchar(50)");
            //    options.Property(roleClaim => roleClaim.ClaimValue).HasColumnType("varchar(50)");

            //    options.ToTable("ApplicationClaim");
            //});

            //builder.Entity<IdentityUserLogin<Guid>>(options =>
            //{
            //    options.ToTable("ApplicationUserLogin");

            //    options.HasKey(userLogin => new { userLogin.ProviderKey, userLogin.LoginProvider });

            //    options.Property(userLogin => userLogin.LoginProvider).HasColumnType("varchar(128)");
            //    options.Property(userLogin => userLogin.ProviderKey).HasColumnType("varchar(128)");
            //    options.Property(userLogin => userLogin.ProviderDisplayName).HasColumnType("varchar(50)");
            //});

            //builder.Entity<IdentityUserToken<Guid>>(options =>
            //{
            //    options.ToTable("ApplicationUserToken");

            //    options.HasKey(userToken => new { userToken.UserId, userToken.LoginProvider, userToken.Name });

            //    options.Property(userToken => userToken.LoginProvider).HasColumnType("varchar(128)");
            //    options.Property(userToken => userToken.Name).HasColumnType("varchar(128)");
            //    options.Property(userToken => userToken.Value).HasColumnType("varchar(256)");
            //});

            //builder.Entity<IdentityRoleClaim<Guid>>(options =>
            //{
            //    options.ToTable("ApplicationRoleClaim");

            //    options.HasKey(roleClaim => roleClaim.Id);
            //});            

            //builder.Entity<IdentityUserClaim<Guid>>(options =>
            //{
            //    options.ToTable("ApplicationUserClaim");

            //    options.HasKey(userClaim => userClaim.Id);

            //    options.Property(userClaim => userClaim.ClaimValue).HasColumnType("varchar(50)");
            //    options.Property(userClaim => userClaim.ClaimType).HasColumnType("varchar(50)");
            //});
        }
    }
}
