using Argon.Catalog.Application.Commands;
using Argon.Catalog.Domain;
using Argon.Core.Data;
using Argon.Core.Messages;
using FluentValidation.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Application.Handlers
{
    public class CreateProductHandler : RequestHandler<CreateProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorage _fileStorage;

        public CreateProductHandler(
            IUnitOfWork unitOfWork,
            IFileStorage fileStorage)
        {
            _unitOfWork = unitOfWork;
            _fileStorage = fileStorage;
        }

        public override async Task<ValidationResult> Handle(
            CreateProductCommand request, CancellationToken cancellationToken)
        {
            var image = await _fileStorage
                .AddAsync(request.Image!.OpenReadStream(), request.Image.FileName, cancellationToken);

            var service = new Product(request.Name, request.Description, 
                request.Price, image.ImageUrl, request.RestaurantId);

            await _unitOfWork.ServiceRepository.AddAsync(service, cancellationToken);
            await _unitOfWork.CommitAsync();

            return ValidationResult;
        }
    }
}
