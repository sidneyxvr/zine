using Argon.Identity.Data;
using Argon.Identity.Models;
using Argon.Identity.Services;
using Argon.WebApp.API.TemplateEmails;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;

namespace Argon.WebApp.API.Configurations
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection RegisterIdentity(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.TryAddScoped<IAccountService, AccountService>();
            services.TryAddScoped<IAuthService, AuthService>();
            services.TryAddScoped<ITokenService, JwtService>();
            services.TryAddScoped<IRefreshTokenStore, RefreshTokenStore>();
            services.TryAddScoped<IEmailService, IdentityEmailService>();

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);

                if (env.IsEnvironment("Testing"))
                {
                    options.SignIn.RequireConfirmedEmail = true;
                }

                options.User.RequireUniqueEmail = true;
            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<IdentityContext>();

            return services;
        }
    }
}
