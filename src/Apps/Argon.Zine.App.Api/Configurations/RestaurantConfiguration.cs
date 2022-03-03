using Argon.Restaurants.Application.Commands;
using Argon.Restaurants.Application.Handlers;
using Argon.Restaurants.Application.Validators;
using Argon.Restaurants.Domain;
using Argon.Restaurants.Infra.Data;
using Argon.Restaurants.Infra.Data.Repositories;
using Argon.Zine.Commom;
using Argon.Zine.Commom.Messages.IntegrationCommands;
using Argon.Zine.Restaurants.Infra.Data.Queries;
using Argon.Zine.Restaurants.QueryStack.Queries;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Argon.Zine.App.Api.Configurations;

public static class RestaurantConfiguration
{
    public static IServiceCollection RegisterSupplier(this IServiceCollection services)
    {
        services.TryAddTransient<IValidator<CreateRestaurantCommand>, CreateRestaurantValidator>();
        services.TryAddTransient<IValidator<UpdateAddressCommand>, UpdateAddressValidator>();

        services.TryAddScoped<IRequestHandler<CreateRestaurantCommand, AppResult>, CreateRestaurantHandler>();
        services.TryAddScoped<IRequestHandler<OpenRestaurantCommand, AppResult>, OpenRestaurantHandler>();
        services.TryAddScoped<IRequestHandler<CloseRestaurantCommand, AppResult>, CloseRestaurantHandler>();

        services.TryAddScoped<IRestaurantRepository, RestaurantRepository>();
        services.TryAddScoped<IUnitOfWork, UnitOfWork>();

        services.TryAddScoped<IRestaurantQueries, RestaurantQueries>();

        return services;
    }
}