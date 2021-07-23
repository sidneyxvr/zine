using Argon.Basket.Services;
using Argon.Core.Communication;
using Argon.Core.Data.EventSourcing;
using Argon.Core.DomainObjects;
using Argon.EventSourcing;
using Argon.WebApp.API.Extensions;
using EventStore.ClientAPI;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using System;
using System.Net;
using System.Net.Mail;

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

            services.AddScoped<IAppUser, AppUser>();
            services.AddScoped<IBasketService, BasketService>();

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

            BsonClassMap.RegisterClassMap<Basket.Models.Basket>(cm =>
            {
                cm.AutoMap();
                cm.MapField("_products").SetElementName("Products");
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
