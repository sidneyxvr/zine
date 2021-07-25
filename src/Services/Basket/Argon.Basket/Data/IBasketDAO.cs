using Argon.Basket.Models;
using System;
using System.Threading.Tasks;

namespace Argon.Basket.Data
{
    public interface IBasketDAO
    {
        Task AddAsync(CustomerBasket basket);
        Task UpdateAsync(CustomerBasket basket);
        Task<CustomerBasket> GetByCustomerIdAsync(Guid customerId);
    }
}
