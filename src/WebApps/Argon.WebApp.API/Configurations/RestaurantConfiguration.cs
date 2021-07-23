using Argon.Core.Messages.IntegrationCommands;
using Argon.Restaurants.Application.Validators;
using Argon.Restaurants.Application.Commands;
using Argon.Restaurants.Domain;
using Argon.Restaurants.Infra.Data;
using Argon.Restaurants.Infra.Data.Repositories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using FluentValidation.Results;
using MediatR;
using Argon.Restaurants.Application.Handlers;

namespace Argon.WebApp.API.Configurations
{
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

            return services;
        }
    }
}
