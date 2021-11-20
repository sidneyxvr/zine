using Argon.Restaurants.Application.Commands;
using Argon.Restaurants.Application.Handlers;
using Argon.Restaurants.Application.Validators;
using Argon.Restaurants.Domain;
using Argon.Restaurants.Infra.Data;
using Argon.Restaurants.Infra.Data.Repositories;
using Argon.Zine.Core.Messages.IntegrationCommands;
using Argon.Zine.Restaurants.Infra.Data.Queries;
using Argon.Zine.Restaurants.QueryStack.Queries;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Argon.Zine.App.Api.Configurations;

public static class RestaurantConfiguration
{
    public static IServiceCollection RegisterSupplier(this IServiceCollection services)
    {
        services.TryAddTransient<IValidator<CreateRestaurantCommand>, CreateRestaurantValidator>();
        services.TryAddTransient<IValidator<UpdateAddressCommand>, UpdateAddressValidator>();

        services.TryAddScoped<IRequestHandler<CreateRestaurantCommand, ValidationResult>, CreateRestaurantHandler>();
        services.TryAddScoped<IRequestHandler<OpenRestaurantCommand, ValidationResult>, OpenRestaurantHandler>();
        services.TryAddScoped<IRequestHandler<CloseRestaurantCommand, ValidationResult>, CloseRestaurantHandler>();

        services.TryAddScoped<IRestaurantRepository, RestaurantRepository>();
        services.TryAddScoped<IUnitOfWork, UnitOfWork>();

        services.TryAddScoped<IRestaurantQueries, RestaurantQueries>();

        return services;
    }
}