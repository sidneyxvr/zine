using Argon.Core.Messages.IntegrationCommands;
using Argon.Customers.Application.Commands;
using Argon.Customers.Application.Handlers;
using Argon.Customers.Application.Validators;
using Argon.Customers.Domain;
using Argon.Customers.Infra.Data;
using Argon.Customers.Infra.Data.Queries;
using Argon.Customers.Infra.Data.Repositories;
using Argon.Customers.QueryStack.Queries;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Argon.WebApp.API.Configurations
{
    public static class CustomerConfiguration
    {
        public static IServiceCollection RegisterCustomer(this IServiceCollection services)
        {
            services.TryAddTransient<IValidator<CreateCustomerCommand>, CreateCustomerValidator>();
            services.TryAddTransient<IValidator<UpdateCustomerCommand>, UpdateCustomerValidator>();
            services.TryAddTransient<IValidator<CreateAddressCommand>, CreateAddressValidator>();
            services.TryAddTransient<IValidator<DefineMainAddressCommand>, DefineMainAddressValidator>();
            services.TryAddTransient<IValidator<DeleteAddressCommand>, DeleteAddressValidator>();
            services.TryAddTransient<IValidator<UpdateAddressCommand>, UpdateAddressValidator>();

            services.TryAddScoped<IRequestHandler<CreateCustomerCommand, ValidationResult>, CreateCustomerHandler>();
            services.TryAddScoped<IRequestHandler<CreateAddressCommand, ValidationResult>, CreateAddressHandler>();
            services.TryAddScoped<IRequestHandler<UpdateAddressCommand, ValidationResult>, UpdateAddressHandler>();
            services.TryAddScoped<IRequestHandler<DeleteAddressCommand, ValidationResult>, DeleteAddressHandler>();
            services.TryAddScoped<IRequestHandler<DefineMainAddressCommand, ValidationResult>, DefineMainAddressHandler>();

            services.TryAddScoped<ICustomerQuery, CustomerQuery>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}
