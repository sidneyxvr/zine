using Argon.Core.Communication;
using Argon.Core.Messages.IntegrationCommands;
using Argon.Customers.Application.CommandHandlers.AddressHandlers;
using Argon.Customers.Application.CommandHandlers.CustomerHandlers;
using Argon.Customers.Application.Commands.AddressCommands;
using Argon.Customers.Domain;
using Argon.Customers.Infra.Data;
using Argon.Customers.Infra.Data.Repositories;
using Argon.Identity.Configurations;
using Argon.Identity.Data;
using Argon.Identity.Services;
using Argon.WebApi.API.Extensions;
using Argon.WebApi.API.TemplateEmails;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Argon.WebApi.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IBus, InMemoryBus>();

            //Customers
            services.AddScoped<IRequestHandler<CreateCustomerCommand, ValidationResult>, CreateCustomerHandler>();
            services.AddScoped<IRequestHandler<CreateAddressCommand, ValidationResult>, CreateAddressHandler>();
            services.AddScoped<IRequestHandler<UpdateAddressCommand, ValidationResult>, UpdateAddressHandler>();
            services.AddScoped<IRequestHandler<DeleteAddressCommand, ValidationResult>, DeleteAddressHandler>();
            services.AddScoped<IRequestHandler<DefineMainAddressCommand, ValidationResult>, DefineMainAddressHandler>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<CustomerContext>();

            //Identity
            var jwtSettingsSection = configuration.GetSection(nameof(JwtSettings));
            services.Configure<JwtSettings>(jwtSettingsSection);

            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = jwtSettings.ValidOn,
                ValidIssuer = jwtSettings.Emitter
            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
            });

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, JwtService>();
            services.AddScoped<IRefreshTokenStore, RefreshTokenStore>();
            services.AddScoped<IEmailService, IdentityEmailService>();
            //services.AddScoped<Identity.Managers.SignInManager>();

            var emailSenderSettingsSection = configuration.GetSection(nameof(EmailSenderSettings));
            var emailSenderSettings = emailSenderSettingsSection.Get<EmailSenderSettings>();
            services.AddFluentEmail(emailSenderSettings.Email)
                .AddRazorRenderer()
                .AddSmtpSender(new SmtpClient
                {
                    Host = emailSenderSettings.Host,
                    Port = emailSenderSettings.Port,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailSenderSettings.Email, emailSenderSettings.Password)
                });

            services.AddScoped<IdentityContext>();

            return services;
        }
    }
}
