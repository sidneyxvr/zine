using Argon.Zine.Ordering.Application.Commands;
using Argon.Zine.Ordering.Application.Handlers;
using Argon.Zine.Ordering.Domain;
using Argon.Zine.Ordering.Infra.Data;
using Argon.Zine.Ordering.Infra.Data.Repositories;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Argon.Zine.App.Api.Configurations
{
    public static class OrderingConfiguration
    {
        public static IServiceCollection RegisterOrdering(this IServiceCollection services)
        {
            services.TryAddScoped<IRequestHandler<AddPaymentMethodCommand, ValidationResult>, AddPaymentMethodHandler>();
            services.TryAddScoped<IRequestHandler<SubmitOrderCommand, ValidationResult>, SubmitOrderHandler>();

            services.TryAddScoped<IBuyerRepository, BuyerRepository>();
            services.TryAddScoped<IOrderRepository, OrderRepository>();
            services.TryAddScoped<IRestaurantRepository, RestaurantRepository>();

            services.TryAddScoped<IUnitOfWork, UnitOfWork>();

            services.TryAddScoped<ISequencialIdentifier, SequencialIdentifier>();

            return services;
        }
    }
}
