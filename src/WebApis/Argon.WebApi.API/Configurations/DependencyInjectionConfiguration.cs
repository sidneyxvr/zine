using Argon.Core.Communication;
using Argon.Core.Messages.IntegrationCommands;
using Argon.Customers.Application.CommandHandlers.CustomerHandlers;
using Argon.Customers.Application.EventHandlers.CustomersHandlers;
using Argon.Customers.Domain.AggregatesModel.CustomerAggregate;
using Argon.Customers.Domain.Events;
using Argon.Customers.Infra.Data;
using Argon.Customers.Infra.Data.Repositories;
using Argon.Identity.Application.CommandHandlers;
using Argon.Identity.Application.Commands;
using Argon.Identity.Application.Data;
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

            services.AddScoped<INotificationHandler<CreatedCustomerEvent>, CreatedCustomerHandler>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<CustomerContext>();

            //Identity
            services.AddScoped<IRequestHandler<CreateUserCommand, ValidationResult>, CreateUserHandler>();

            services.AddScoped<IdentityContext>();

            return services;
        }
    }
}
