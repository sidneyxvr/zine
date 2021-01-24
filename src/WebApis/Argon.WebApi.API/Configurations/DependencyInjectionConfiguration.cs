using Argon.Core.Communication;
using Argon.Customers.Application.CommandHandlers.CustomerHandlers;
using Argon.Customers.Application.Commands.CustomerCommands;
using Argon.Customers.Application.EventHandlers.CustomersHandlers;
using Argon.Customers.Domain.AggregatesModel.CustomerAggregate;
using Argon.Customers.Domain.Events;
using Argon.Customers.Infra.Data.Repositories;
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

            services.AddScoped<IRequestHandler<CreateCustomerCommand, ValidationResult>, CreateCustomerHandler>();

            services.AddScoped<INotificationHandler<CreatedCustomerEvent>, CreatedCustomerHandler>();

            services.AddScoped<IBus, InMemoryBus>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}
