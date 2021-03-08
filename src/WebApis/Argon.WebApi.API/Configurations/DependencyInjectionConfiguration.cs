using Argon.Core.Communication;
using Argon.Core.Messages.IntegrationCommands;
using Argon.Customers.Application.CommandHandlers.AddressHandlers;
using Argon.Customers.Application.CommandHandlers.CustomerHandlers;
using Argon.Customers.Application.Commands.AddressCommands;
using Argon.Customers.Application.EventHandlers.CustomersHandlers;
using Argon.Customers.Domain.AggregatesModel.CustomerAggregate;
using Argon.Customers.Domain.Events;
using Argon.Customers.Infra.Data;
using Argon.Customers.Infra.Data.Repositories;
using Argon.Identity.Data;
using Argon.Identity.Services;
using Argon.WebApi.API.Extensions;
using Argon.WebApi.API.TemplateEmails;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Mail;

namespace Argon.WebApi.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(Startup).Assembly);

            services.AddScoped<IBus, InMemoryBus>();

            //Customers
            services.AddScoped<IRequestHandler<CreateCustomerCommand, ValidationResult>, CreateCustomerHandler>();
            services.AddScoped<IRequestHandler<CreateAddressCommand, ValidationResult>, CreateAddressHandler>();
            services.AddScoped<IRequestHandler<UpdateAddressCommand, ValidationResult>, UpdateAddressHandler>();
            services.AddScoped<IRequestHandler<DeleteAddressCommand, ValidationResult>, DeleteAddressHandler>();
            services.AddScoped<IRequestHandler<DefineMainAddressCommand, ValidationResult>, DefineMainAddressHandler>();

            services.AddScoped<INotificationHandler<CreatedCustomerEvent>, CreatedCustomerHandler>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<CustomerContext>();

            //Identity
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
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

            services.AddScoped<IdentityContext>();

            return services;
        }
    }
}
