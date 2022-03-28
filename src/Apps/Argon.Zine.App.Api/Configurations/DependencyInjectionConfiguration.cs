using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Argon.Storage;
using Argon.Zine.App.Api.Extensions;
using Argon.Zine.Commom.Communication;
using Argon.Zine.Commom.Data;
using Argon.Zine.Commom.DomainObjects;
using Argon.Zine.Storage;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Security.Claims;

namespace Argon.Zine.App.Api.Configurations;

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
            var user = provider.GetRequiredService<IHttpContextAccessor>().HttpContext?.User;

            if (user is null)
            {
                throw new NullReferenceException(nameof(user));
            }

            var id = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var firstName = user.FindFirstValue(ClaimTypes.GivenName);
            var lastName = user.FindFirstValue(ClaimTypes.Surname);

            return new AppUser(Guid.Parse(id), firstName, lastName);
        });

        var s3SettingsSection = configuration.GetSection(nameof(S3Settings));
        services.Configure<S3Settings>(s3SettingsSection);

        var s3Settings = s3SettingsSection.Get<S3Settings>();

        services.AddScoped<AmazonS3Client>(provider
            => new AmazonS3Client(s3Settings.AccessId, s3Settings.AccessKey, RegionEndpoint.GetBySystemName(s3Settings.Region)));

        services.AddScoped<TransferUtility>(provider
            => new TransferUtility(provider.GetRequiredService<AmazonS3Client>()));

        services.TryAddScoped<IFileStorage>(provider
            => new FileStorage(s3Settings.BucketName, provider.GetRequiredService<TransferUtility>()));

        return services;
    }
}