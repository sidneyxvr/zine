using Argon.Zine.Catalog.Application.Commands;
using Argon.Zine.Catalog.Communication.Events;
using Argon.Zine.Catalog.Domain;
using Argon.Zine.Core.Data;
using Argon.Zine.Core.Messages;
using FluentValidation.Results;
using Microsoft.Extensions.Localization;

namespace Argon.Zine.Catalog.Application.Handlers
{
    public class CreateProductHandler : RequestHandler<CreateProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorage _fileStorage;
        private readonly IStringLocalizer<CreateProductHandler> _localizer;

        public CreateProductHandler(
            IUnitOfWork unitOfWork,
            IFileStorage fileStorage,
            IStringLocalizer<CreateProductHandler> localizer)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
            _fileStorage = fileStorage;
        }

        public override async Task<ValidationResult> Handle(
            CreateProductCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _unitOfWork.RestaurantRepository
                .GetByIdAsync(request.RestaurantId);

            if(restaurant is null)
            {
                return WithError(_localizer["Restaurant Not Found"]);
            }

            var image = await _fileStorage
                .AddAsync(request.Image!.OpenReadStream(), request.Image.FileName, cancellationToken);

            var product = new Product(request.Name, request.Description, 
                request.Price, image.Url, request.RestaurantId);

            product.AddDomainEvent(new ProductCreatedEvent(product.Id,
                product.Name, product.Price, product.ImageUrl,
                restaurant.Id, restaurant.Name, restaurant.LogoUrl));

            await _unitOfWork.ProductRepository.AddAsync(product, cancellationToken);
            await _unitOfWork.CommitAsync();

            return ValidationResult;
        }
    }
}
