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
using Argon.Identity.Application.Data;
using Argon.Identity.Application.Services;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Argon.WebApi.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
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

            services.AddScoped<IdentityContext>();

            return services;
        }
    }
}
