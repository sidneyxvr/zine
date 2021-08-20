using Argon.Chat.Repositories;
using Argon.Chat.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Argon.App.Api.Configurations
{
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
}
