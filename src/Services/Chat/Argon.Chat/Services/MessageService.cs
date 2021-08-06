using Argon.Chat.Models;
using Argon.Chat.Repositories;
using Argon.Chat.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Argon.Chat.Services
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

        private async Task Populate()
        {
            var cUser = new User(new Guid("969EEF64-27EE-447A-42D0-08D94FDA4B97"), "Sidney", "teste");
            var room = new Room("#1234", cUser, new User(Guid.NewGuid(), "Restaurante 1"));

            await _roomRepository.AddAsync(room);
            
            await _messageRepository.AddAsync(new Message(room.Id, cUser, "Mensagem 1", DateTime.UtcNow));
            await _messageRepository.AddAsync(new Message(room.Id, cUser, "Mensagem 2", DateTime.UtcNow));
            await _messageRepository.AddAsync(new Message(room.Id, cUser, "Mensagem 3", DateTime.UtcNow));
            await _messageRepository.AddAsync(new Message(room.Id, cUser, "Mensagem 4", DateTime.UtcNow));
            await _messageRepository.AddAsync(new Message(room.Id, cUser, "Mensagem 5", DateTime.UtcNow));
        }
    }
}
