﻿using Argon.Restaurants.Domain;
using Argon.Zine.Core.Data;
using Argon.Zine.Core.DomainObjects;
using Argon.Zine.Core.Messages;
using Argon.Zine.Core.Messages.IntegrationCommands;
using Argon.Zine.Core.Messages.IntegrationEvents;
using FluentValidation.Results;

namespace Argon.Restaurants.Application.Handlers;

public class CreateRestaurantHandler : RequestHandler<CreateRestaurantCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorage _fileStorage;

    public CreateRestaurantHandler(
        IUnitOfWork unitOfWork,
        IFileStorage fileStorage)
    {
        _unitOfWork = unitOfWork;
        _fileStorage = fileStorage;
    }

    public override async Task<ValidationResult> Handle(
        CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var name = new Name(request.CorparateName!, request.CorparateName!);
        var user = new User(request.UserId, name, request.Email);

        var location = new Location(request.Latitude!.Value, request.Longitude!.Value);
        var address = new Address(request.Street!, request.Number!, request.District!, 
            request.City!, request.State!, request.PostalCode!, location);

        var logoUrl = request.Logo is { Length: > 0 }
            ? (await _fileStorage.UploadAsync(request.Logo.OpenReadStream(), request.Logo.FileName, cancellationToken)).Url
            : null;

        var restaurant = new Restaurant(request.CorparateName!, request.TradeName!, request.CpfCnpj!, user, address);
        restaurant.SetLogo(logoUrl);

        var supplierCreatedEvent = new RestaurantCreatedEvent(restaurant.Id, restaurant.TradeName,
            address.Location.Latitude, address.Location.Longitude, address.ToString(), logoUrl);

        restaurant.AddDomainEvent(supplierCreatedEvent);

        await _unitOfWork.RestaurantRepository.AddAsync(restaurant, cancellationToken);
        await _unitOfWork.CommitAsync();

        return ValidationResult;
    }
}