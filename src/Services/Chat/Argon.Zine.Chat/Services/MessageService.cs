using Argon.Zine.Chat.Models;
using Argon.Zine.Chat.Repositories;
using Argon.Zine.Chat.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Argon.Zine.Chat.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMessageRepository _messageRepository;

        public MessageService(
            IRoomRepository roomRepository,
            IMessageRepository messageRepository)
        {
            _roomRepository = roomRepository;
            _messageRepository = messageRepository;
        }

        public async Task<(Guid SenderId, Guid ReceiverId)> AddAsync(SendMessageRequest request)
        {
            var sender = new User(request.SenderId, request.SenderName!);
            var message = new Message(request.RoomId, sender, request.Content, DateTime.UtcNow);

            await _messageRepository.AddAsync(message);

            var room = await SetLastMessageRoomAsync(request.RoomId, message);

            var senderId = room.CustomerUser.Id == request.SenderId
                ? room.CustomerUser.Id 
                : room.RestaurantUser.Id;

            var receiverId = room.CustomerUser.Id == request.SenderId
                ? room.RestaurantUser.Id
                : room.CustomerUser.Id;

            return (senderId, receiverId);
        }

        private async Task<Room> SetLastMessageRoomAsync(Guid roomId, Message message)
        {
            var room = await _roomRepository.GetByIdAsync(roomId);

            room.SetLastMessage(message);

            await _roomRepository.UpdateAsync(room);

            return room;
        }

        public async Task<IEnumerable<Message>> GetPagedMessagesAsync(GetPagedMessagesRequest request)
            => await _messageRepository.GetPagedAsync(request.UserId, request.Limit, request.Offset);
    }
}
