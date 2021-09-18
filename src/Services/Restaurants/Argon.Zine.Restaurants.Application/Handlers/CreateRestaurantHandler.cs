using Argon.Zine.Core.Data;
using Argon.Zine.Core.Messages;
using Argon.Zine.Core.Messages.IntegrationCommands;
using Argon.Zine.Core.Messages.IntegrationEvents;
using Argon.Restaurants.Domain;
using FluentValidation.Results;
using System.Threading.Tasks;
using System.Threading;

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
        var user = new User(request.UserId, request.CorparateName, request.CorparateName, request.Email);

        var address = new Address(request.Street, request.Number, request.District, request.City,
            request.State, request.PostalCode, request.Complement, request.Latitude, request.Longitude);

        var logoUrl = request.Logo is not null and { Length: > 0 }
            ? (await _fileStorage.AddAsync(request.Logo.OpenReadStream(), request.Logo.FileName, cancellationToken)).ImageUrl
            : null;

        var supplier = new Restaurant(request.CorparateName, request.TradeName, request.CpfCnpj, logoUrl, user, address);

        var supplierCreatedEvent = new RestaurantCreatedEvent(supplier.Id, supplier.TradeName,
            address.Location.Latitude, address.Location.Longitude, address.ToString(), logoUrl);

        supplier.AddDomainEvent(supplierCreatedEvent);

        await _unitOfWork.RestaurantRepository.AddAsync(supplier, cancellationToken);
        await _unitOfWork.CommitAsync();

        return ValidationResult;
    }
}