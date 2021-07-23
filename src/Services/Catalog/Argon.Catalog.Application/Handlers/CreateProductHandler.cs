﻿using Argon.Catalog.Application.Commands;
using Argon.Catalog.Communication.Events;
using Argon.Catalog.Domain;
using Argon.Core.Data;
using Argon.Core.Messages;
using FluentValidation.Results;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Application.Handlers
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

            //var image = await _fileStorage
            //    .AddAsync(request.Image!.OpenReadStream(), request.Image.FileName, cancellationToken);

            var product = new Product(request.Name, "teste", 
                request.Price, "teste", request.RestaurantId);

            product.AddDomainEvent(new ProductCreatedEvent(product.Id,
                product.Name, product.Price, product.ImageUrl, 
                restaurant.Id, restaurant.Name, restaurant.LogoUrl));

            await _unitOfWork.ProductRepository.AddAsync(product, cancellationToken);
            await _unitOfWork.CommitAsync();

            return ValidationResult;
        }
    }
}
