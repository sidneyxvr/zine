using Argon.Basket.Requests;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace Argon.Basket.Services
{
    public interface IBasketService
    {
        Task<ValidationResult> AddProductToBasket(ProductToBasketDTO product);
    }
}
