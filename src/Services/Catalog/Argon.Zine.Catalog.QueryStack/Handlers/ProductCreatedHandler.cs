using Argon.Zine.Catalog.Communication.Events;
using Argon.Zine.Catalog.QueryStack.Models;
using Argon.Zine.Catalog.QueryStack.Services;
using Argon.Zine.Core.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Zine.Catalog.QueryStack.Handlers
{
    public class ProductCreatedHandler : NotificationHandler<ProductCreatedEvent>
    {
        private readonly IProductService _productService;

        public ProductCreatedHandler(IProductService productService)
            => _productService = productService;

        public override async Task Handle(
            ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            var restaurant = new RestaurantProduct(notification.RestaurantId, 
                notification.Name, notification.RestaurantLogo);

            var product = new Product(notification.AggregateId, notification.Name, 
                notification.Price, notification.ImageUrl, restaurant);

            await _productService.AddAsync(product);
        }
    }
}