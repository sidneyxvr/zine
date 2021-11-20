using Argon.Zine.Chat.Repositories;
using Argon.Zine.Chat.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Argon.Zine.App.Api.Configurations;

public static class ChatConfiguration
{
    public static IServiceCollection RegisterChat(this IServiceCollection services)
    {
        services.TryAddScoped<IUserService, UserService>();
        services.TryAddScoped<IMessageService, MessageService>();
        services.TryAddScoped<IRoomService, RoomService>();

        services.TryAddScoped<IMessageRepository, MessageRepository>();
        services.TryAddScoped<IRoomRepository, RoomRepository>();

        return services;
    }
}