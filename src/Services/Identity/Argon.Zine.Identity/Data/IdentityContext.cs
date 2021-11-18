using Argon.Zine.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Argon.Zine.Identity.Data
{
    public class IdentityContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
#pragma warning disable CS8618 
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
#pragma warning restore CS8618 
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(
                new Role { Id = new Guid("3215CA3D-EC71-4DF4-BF41-555FFCC04F22"), Name = "Customer", NormalizedName = "CUSTOMER", ConcurrencyStamp = "a76ddbb4-c77f-410f-83cd-9d66e3dc893e" },
                new Role { Id = new Guid("07E778B6-7C1D-4852-B1F4-63661E4BB08A"), Name = "Restaurant", NormalizedName = "RESTAURANT", ConcurrencyStamp = "11a13445-7bba-4fad-86b1-ac3cef32e569" },
                new Role { Id = new Guid("78DF47E2-8160-4678-99B0-2E80492268DD"), Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "bc6e8e27-8e7f-413d-bd2d-982b4d80a801" });

            builder.Entity<User>(options =>
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

            builder.Entity<Role>(options =>
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

            builder.Entity<RefreshToken>(options =>
            {
                options.ToTable(nameof(RefreshToken));

                options.HasKey(userRole => userRole.Token);

                options.Property(refreshToken => refreshToken.Token)
                    .IsUnicode(false)
                    .HasMaxLength(128);

                options.Property(refreshToken => refreshToken.ConcurrencyStamp)
                    .IsUnicode(false)
                    .HasMaxLength(36);

                options.Property(refreshToken => refreshToken.UserId)
                    .IsRequired();

                options.Property(refreshToken => refreshToken.CreatedAt)
                    .IsRequired();

                options.Property(refreshToken => refreshToken.Token)
                    .IsRequired();

                options.Property(refreshToken => refreshToken.ValidityInHours)
                    .IsRequired();
            });

            //builder.Ignore<IdentityRoleClaim<Guid>>();
            builder.Ignore<IdentityUserLogin<Guid>>();
            builder.Ignore<IdentityUserToken<Guid>>();
            //builder.Ignore<IdentityUserClaim<Guid>>();

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

            builder.Entity<IdentityRoleClaim<Guid>>(options =>
            {
                options.ToTable("RoleClaim");

                options.HasKey(roleClaim => roleClaim.Id);
            });

            builder.Entity<IdentityUserClaim<Guid>>(options =>
            {
                options.ToTable("UserClaim");

                options.HasKey(userClaim => userClaim.Id);

                options.Property(userClaim => userClaim.ClaimValue).HasColumnType("varchar(50)");
                options.Property(userClaim => userClaim.ClaimType).HasColumnType("varchar(50)");
            });
        }
    }
}
