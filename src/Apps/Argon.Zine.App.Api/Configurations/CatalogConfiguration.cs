using Argon.Storage;
using Argon.Zine.Catalog.Application.Commands;
using Argon.Zine.Catalog.Application.Handlers;
using Argon.Zine.Catalog.Application.Validators;
using Argon.Zine.Catalog.Domain;
using Argon.Zine.Catalog.Infra.Data;
using Argon.Zine.Catalog.Infra.Data.Queries;
using Argon.Zine.Catalog.Infra.Data.Repositories;
using Argon.Zine.Catalog.QueryStack.Queries;
using Argon.Zine.Core.Data;
using Argon.Zine.Core.Messages.IntegrationEvents;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Data;

namespace Argon.Zine.App.Api.Configurations
{
    public static class CatalogConfiguration
    {
        public static IServiceCollection RegisterCatalog(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddTransient<IValidator<CreateCategoryCommand>, CreateCategoryValidator>();
            services.TryAddTransient<IValidator<CreateProductCommand>, CreateProductValidator>();

            services.TryAddScoped<IRequestHandler<CreateCategoryCommand, ValidationResult>, CreateCategoryHandler>();
            services.TryAddScoped<IRequestHandler<CreateProductCommand, ValidationResult>, CreateProductHandler>();

            services.AddScoped<INotificationHandler<RestaurantCreatedEvent>, RestaurantCreatedHandler>();

            services.AddScoped<INotificationHandler<OpenRestaurantEvent>, OpenRestaurantHandler>();

            services.AddScoped<INotificationHandler<ClosedRestaurantEvent>, ClosedRestaurantHandler>();


            //services.TryDecorate<IRestaurantQueries, RestaurantCache>();

            services.TryAddScoped<IUnitOfWork, UnitOfWork>();
            services.TryAddScoped<ICategoryRepository, CategoryRepository>();
            services.TryAddScoped<IProductRepository, ProductRepository>();
            services.TryAddScoped<IRestaurantRepository, RestaurantRepository>();
            
            services.TryAddScoped<IProductQueries, ProductQueries>();
            services.TryAddScoped<IRestaurantQueries, RestaurantQueries>();

            services.TryAddScoped<IFileStorage, FileStorage>();

            services.TryAddScoped<IDbConnection>(provider
                => new SqlConnection(configuration.GetConnectionString("CatalogConnection")));

            return services;
        }
    }
}
