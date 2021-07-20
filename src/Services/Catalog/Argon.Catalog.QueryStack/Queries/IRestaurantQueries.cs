using Argon.Catalog.QueryStack.Models;
using System;
using System.Threading.Tasks;

namespace Argon.Catalog.QueryStack.Queries
{
    public interface IRestaurantQueries
    {
        Task<Restaurant?> GetByIdAsync(Guid id);
    }
}
