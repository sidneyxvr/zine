using Argon.Basket.Data;
using Argon.Basket.Models;
using Argon.Basket.Services;
using Argon.Core.Communication;
using Argon.Core.Data.EventSourcing;
using Argon.Core.DomainObjects;
using Argon.EventSourcing;
using Argon.WebApp.API.Extensions;
using EventStore.ClientAPI;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using System.Net;
using System.Net.Mail;
using System.Security.Principal;

namespace Argon.WebApp.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            //General
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IBus, InMemoryBus>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));

            services.AddScoped<IAppUser>(provider => 
            {
                var httpContext = provider.GetRequiredService<IHttpContextAccessor>();

                return new AppUser(httpContext);
            });

            services.AddScoped<IBasketService, BasketService>();
            services.AddSingleton<IBasketDAO, BasketDAO>();

            services.AddSingleton<IEventSourcingStorage, EventSourcingStorage>();
            services.AddSingleton<IEventStoreConnection>(provider => {
                var settings = ConnectionSettings.Create()
                    .DisableTls()
                    .UseDebugLogger()
                    .SetMaxDiscoverAttempts(1)
                    .EnableVerboseLogging();

                var connection = EventStoreConnection.Create("ConnectTo=tcp://admin:changeit@localhost:1113", settings);

                return connection;
            });

            BsonClassMap.RegisterClassMap<CustomerBasket>(cm =>
            {
                cm.AutoMap();
                cm.MapProperty(b => b.RestaurantLogoUrl)
                    .SetIgnoreIfNull(true);
                cm.MapField("_products")
                    .SetElementName("Products");
            });

            var emailSenderSettingsSection = configuration.GetSection(nameof(EmailSenderSettings));
            var emailSenderSettings = emailSenderSettingsSection.Get<EmailSenderSettings>();
            services.AddFluentEmail(emailSenderSettings.Email)
                .AddRazorRenderer()
                .AddSmtpSender(new SmtpClient
                {
                    Host = emailSenderSettings.Host,
                    Port = emailSenderSettings.Port,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailSenderSettings.Email, emailSenderSettings.Password)
                });

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("CatalogRedis");
            });

            return services;
        }
    }
}
