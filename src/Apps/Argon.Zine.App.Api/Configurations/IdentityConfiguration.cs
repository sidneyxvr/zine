using Argon.Zine.Identity.Data;
using Argon.Zine.Identity.Models;
using Argon.Zine.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RabbitMQ.Client;

namespace Argon.Zine.App.Api.Configurations;

public static class IdentityConfiguration
{
    public static IServiceCollection RegisterIdentity(this IServiceCollection services, IWebHostEnvironment env)
    {
        services.TryAddScoped<IAccountService, AccountService>();
        services.TryAddScoped<IAuthService, AuthService>();
        services.TryAddScoped<ITokenService, JwtService>();
        services.TryAddScoped<IRefreshTokenStore, RefreshTokenStore>();

        services.TryAddSingleton<IConnectionFactory>(_
            => new ConnectionFactory() { HostName = "localhost" });

        services.TryAddSingleton<IConnection>(provider
            => new ConnectionFactory().CreateConnection());


        services.TryAddSingleton<IEmailService>(provider =>
        {
            var connection = provider.GetRequiredService<IConnection>();

            var channel = connection.CreateModel();

            return new EmailService(channel);
        });

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