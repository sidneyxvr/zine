using Argon.Storage;
using Argon.Zine.Catalog.Application.Commands;
using Argon.Zine.Catalog.Application.Handlers;
using Argon.Zine.Catalog.Application.Validators;
using Argon.Zine.Catalog.Communication.Events;
using Argon.Zine.Catalog.Domain;
using Argon.Zine.Catalog.Infra.Caching;
using Argon.Zine.Catalog.Infra.Data;
using Argon.Zine.Catalog.Infra.Data.Queries.Services;
using Argon.Zine.Catalog.Infra.Data.Repositories;
using Argon.Zine.Catalog.QueryStack.Handlers;
using Argon.Zine.Catalog.QueryStack.Queries;
using Argon.Zine.Catalog.QueryStack.Services;
using Argon.Zine.Core.Data;
using Argon.Zine.Core.Messages.IntegrationEvents;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Argon.Zine.App.Api.Configurations
{
    public static class CatalogConfiguration
    {
        public static IServiceCollection RegisterCatalog(this IServiceCollection services)
        {
            services.TryAddTransient<IValidator<CreateCategoryCommand>, CreateCategoryValidator>();
            services.TryAddTransient<IValidator<CreateProductCommand>, CreateProductValidator>();

            services.TryAddScoped<IRequestHandler<CreateCategoryCommand, ValidationResult>, CreateCategoryHandler>();
            services.TryAddScoped<IRequestHandler<CreateProductCommand, ValidationResult>, CreateProductHandler>();
            
            services.AddScoped<INotificationHandler<RestaurantCreatedEvent>, Catalog.Application.Handlers.RestaurantCreatedHandler>();
            services.AddScoped<INotificationHandler<RestaurantCreatedEvent>, Catalog.QueryStack.Handlers.RestaurantCreatedHandler>();

            services.AddScoped<INotificationHandler<OpenRestaurantEvent>, Catalog.Application.Handlers.OpenRestaurantHandler>();
            services.AddScoped<INotificationHandler<OpenRestaurantEvent>, Catalog.QueryStack.Handlers.OpenRestaurantHandler>();

            services.AddScoped<INotificationHandler<ClosedRestaurantEvent>, Catalog.Application.Handlers.ClosedRestaurantHandler>();
            services.AddScoped<INotificationHandler<ClosedRestaurantEvent>, Catalog.QueryStack.Handlers.ClosedRestaurantHandler>();

            services.AddScoped<INotificationHandler<ProductCreatedEvent>, ProductCreatedHandler>();

            services.TryAddSingleton<IRestaurantService, RestaurantService>();
            services.TryAddSingleton<IProductService, ProductService>();

            services.TryDecorate<IRestaurantQueries, RestaurantCache>();
            services.TryDecorate<IProductQueries, ProductCache>();

            services.TryAddScoped<IUnitOfWork, UnitOfWork>();
            services.TryAddScoped<ICategoryRepository, CategoryRepository>();
            services.TryAddScoped<IProductRepository, ProductRepository>();
            services.TryAddScoped<IRestaurantRepository, RestaurantRepository>();

            services.TryAddScoped<IFileStorage, FileStorage>();

            return services;
        }
    }
}
