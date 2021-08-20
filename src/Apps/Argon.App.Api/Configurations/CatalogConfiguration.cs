using Argon.Catalog.Application.Commands;
using Argon.Catalog.Application.Handlers;
using Argon.Catalog.Application.Validators;
using Argon.Catalog.Caching;
using Argon.Catalog.Communication.Events;
using Argon.Catalog.Domain;
using Argon.Catalog.Infra.Data;
using Argon.Catalog.Infra.Data.Queries.Services;
using Argon.Catalog.Infra.Data.Repositories;
using Argon.Catalog.QueryStack.Cache;
using Argon.Catalog.QueryStack.Handlers;
using Argon.Catalog.QueryStack.Queries;
using Argon.Catalog.QueryStack.Services;
using Argon.Core.Data;
using Argon.Core.Messages.IntegrationEvents;
using Argon.Storage;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Argon.App.Api.Configurations
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

            services.TryAddScoped<IRestaurantQueries, RestaurantQueries>();
            services.TryAddScoped<IProductQueries, ProductQueries>();
            
            services.TryAddScoped<IRestaurantCache, RestaurantCache>();
            services.TryAddScoped<IProductCache, ProductCache>();

            services.TryAddScoped<IUnitOfWork, UnitOfWork>();
            services.TryAddScoped<ICategoryRepository, CategoryRepository>();
            services.TryAddScoped<IProductRepository, ProductRepository>();
            services.TryAddScoped<IRestaurantRepository, RestaurantRepository>();

            services.TryAddScoped<IFileStorage, FileStorage>();

            return services;
        }
    }
}
