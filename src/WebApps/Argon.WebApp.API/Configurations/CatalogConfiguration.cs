using Argon.Catalog.Application.Commands;
using Argon.Catalog.Application.Handlers;
using Argon.Catalog.Application.Queries;
using Argon.Catalog.Application.Validators;
using Argon.Catalog.Domain;
using Argon.Catalog.Infra.Data;
using Argon.Catalog.Infra.Data.Queries;
using Argon.Catalog.Infra.Data.Repositories;
using Argon.Catalog.Infra.Data.Storages;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Argon.WebApp.API.Configurations
{
    public static class CatalogConfiguration
    {
        public static IServiceCollection RegisterCatalog(this IServiceCollection services)
        {
            services.TryAddTransient<IValidator<CreateDepartmentCommand>, CreateDepartmentValidator>();
            services.TryAddTransient<IValidator<CreateCategoryCommand>, CreateCategoryValidator>();
            services.TryAddTransient<IValidator<CreateSubCategoryCommand>, CreateSubCategoryValidator>();
            services.TryAddTransient<IValidator<CreateServiceCommand>, CreateServiceValidator>();

            services.TryAddScoped<IRequestHandler<CreateDepartmentCommand, ValidationResult>, CreateDepartmentHandler>();
            services.TryAddScoped<IRequestHandler<CreateCategoryCommand, ValidationResult>, CreateCategoryHandler>();
            services.TryAddScoped<IRequestHandler<CreateSubCategoryCommand, ValidationResult>, CreateSubCategoryHandler>();
            services.TryAddScoped<IRequestHandler<CreateServiceCommand, ValidationResult>, CreateServiceHandler>();

            services.TryAddScoped<IDepartmentQueries, DepartmentQueries>();

            services.TryAddScoped<IUnitOfWork, UnitOfWork>();
            services.TryAddScoped<IDepartmentRepository, DepartmentRepository>();
            services.TryAddScoped<ICategoryRepository, CategoryRepository>();
            services.TryAddScoped<ISubCategoryRepository, SubCategoryRepository>();
            services.TryAddScoped<IServiceRepository, ServiceRepository>();
            services.TryAddScoped<ISupplierRepository, SupplierRepository>();

            services.TryAddScoped<IFileStorage, FileStorage>();

            return services;
        }
    }
}
