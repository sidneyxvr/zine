using System;
using System.Threading.Tasks;

namespace Argon.Chat.Services
{
    public interface IUserService
    {
        Task<bool> IsConnectedAsync();
        Task<string> GetConnectionIdByUserIdAsync(Guid userId);
    }
}
