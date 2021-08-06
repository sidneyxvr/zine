using System;
using System.Threading.Tasks;

namespace Argon.Chat.Services
{
    public class UserService : IUserService
    {
        public Task<string> GetConnectionIdByUserIdAsync(Guid userId)
            => Task.FromResult("");

        public Task<bool> IsConnectedAsync()
            => Task.FromResult(true);
    }
}
