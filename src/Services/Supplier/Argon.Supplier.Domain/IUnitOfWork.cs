using System.Threading.Tasks;

namespace Argon.Restaurants.Domain
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();

        public IRestaurantRepository RestaurantRepository { get; }
    }
}
