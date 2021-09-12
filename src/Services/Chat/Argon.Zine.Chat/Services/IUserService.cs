using System;
using System.Threading.Tasks;

namespace Argon.Zine.Chat.Services
{
    public interface IUserService
    {
        Task<bool> IsConnectedAsync();
        Task<string> GetConnectionIdByUserIdAsync(Guid userId);
    }
}
