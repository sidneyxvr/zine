using Argon.Core.DomainObjects;

namespace Argon.Chat.Models
{
    public class Room : Entity
    {
        public string Name { get; private set; }
        public User CustomerUser { get; private set; }
        public User RestaurantUser { get; private set; }

        public Message? LastMessage { get; private set; }

        public Room(string name, User customerUser, User restaurantUser)
        {
            Name = name;
            CustomerUser = customerUser;
            RestaurantUser = restaurantUser;
        }

        public void SetLastMessage(Message message)
            => LastMessage = message;
    }
}
