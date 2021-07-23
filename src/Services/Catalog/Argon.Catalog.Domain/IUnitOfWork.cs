using System.Threading.Tasks;

namespace Argon.Catalog.Domain
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();

        public IProductRepository ProductRepository { get; }
        public IRestaurantRepository RestaurantRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
    }
}
