using Argon.Core.Messages.IntegrationCommands;
using Argon.Core.Messages.IntegrationCommands.Validators;
using Argon.Suppliers.Application;
using Argon.Suppliers.Domain;
using Argon.Suppliers.Infra.Data;
using Argon.Suppliers.Infra.Data.Repositories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Argon.WebApp.API.Configurations
{
    public static class SupplierConfiguration
    {
        public static IServiceCollection RegisterSupplier(this IServiceCollection services)
        {
            services.TryAddTransient<IValidator<CreateSupplierCommand>, CreateSupplierValidator>();
            services.TryAddTransient<IValidator<UpdateAddressCommand>, UpdateAddressValidator>();

            services.TryAddScoped<ISupplierRepository, SupplierRepository>();
            services.TryAddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
