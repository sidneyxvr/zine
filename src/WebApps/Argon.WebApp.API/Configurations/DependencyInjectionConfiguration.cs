using Argon.Core.Communication;
using Argon.Core.Messages.IntegrationCommands;
using Argon.Customers.Application.CommandHandlers;
using Argon.Customers.Application.Commands;
using Argon.Customers.Domain;
using Argon.Customers.Infra.Data;
using Argon.Customers.Infra.Data.Queries;
using Argon.Customers.Infra.Data.Repositories;
using Argon.Customers.QueryStack.Queries;
using Argon.Identity.Data;
using Argon.Identity.Services;
using Argon.Suppliers.Application.CommandHandlers;
using Argon.Suppliers.Domain;
using Argon.Suppliers.Infra.Data.Repositories;
using Argon.WebApp.API.Extensions;
using Argon.WebApp.API.TemplateEmails;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Mail;

namespace Argon.WebApp.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Contexts
            services.AddScoped<IdentityContext>();

            //General
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IBus, InMemoryBus>();

            //Customers
            services.AddScoped<CustomerContext>();

            services.AddScoped<IRequestHandler<CreateCustomerCommand, ValidationResult>, CreateCustomerHandler>();
            services.AddScoped<IRequestHandler<CreateAddressCommand, ValidationResult>, CreateAddressHandler>();
            services.AddScoped<IRequestHandler<Customers.Application.Commands.UpdateAddressCommand, ValidationResult>, Customers.Application.CommandHandlers.UpdateAddressHandler>();
            services.AddScoped<IRequestHandler<DeleteAddressCommand, ValidationResult>, DeleteAddressHandler>();
            services.AddScoped<IRequestHandler<DefineMainAddressCommand, ValidationResult>, DefineMainAddressHandler>();

            services.AddScoped<ICustomerQuery, CustomerQuery>();

            services.AddScoped<Customers.Domain.IUnitOfWork, Customers.Infra.Data.UnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            //Suppliers
            services.AddScoped<IRequestHandler<CreateSupplierCommand, ValidationResult>, CreateSupplierHandler>();
            services.AddScoped<IRequestHandler<Suppliers.Application.Commands.UpdateAddressCommand, ValidationResult>, Suppliers.Application.CommandHandlers.UpdateAddressHandler>();

            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<Suppliers.Domain.IUnitOfWork, Suppliers.Infra.Data.UnitOfWork>();

            //Identity
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, JwtService>();
            services.AddScoped<IRefreshTokenStore, RefreshTokenStore>();
            services.AddScoped<IEmailService, IdentityEmailService>();

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

            return services;
        }
    }
}
