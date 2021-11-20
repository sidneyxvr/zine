using Argon.Zine.Core.Messages.IntegrationCommands;
using Argon.Zine.Customers.Application.Commands;
using Argon.Zine.Customers.Application.Handlers;
using Argon.Zine.Customers.Application.Queries;
using Argon.Zine.Customers.Application.Validators;
using Argon.Zine.Customers.Domain;
using Argon.Zine.Customers.Infra.Data;
using Argon.Zine.Customers.Infra.Data.Queries;
using Argon.Zine.Customers.Infra.Data.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Argon.Zine.App.Api.Configurations;

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

        services.TryAddScoped<ICustomerQueries, CustomerQueries>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}