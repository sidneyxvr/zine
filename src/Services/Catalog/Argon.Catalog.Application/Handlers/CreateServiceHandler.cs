using Argon.Catalog.Application.Commands;
using Argon.Catalog.Domain;
using Argon.Core.Messages;
using FluentValidation.Results;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Application.Handlers
{
    public class CreateServiceHandler : RequestHandler<CreateServiceCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorage _storageService;

        public CreateServiceHandler(
            IUnitOfWork unitOfWork,
            IFileStorage storageService)
        {
            _unitOfWork = unitOfWork;
            _storageService = storageService;
        }

        public override async Task<ValidationResult> Handle(
            CreateServiceCommand request, CancellationToken cancellationToken)
        {
            var feeHomeAssistences = request.FeeHomeAssistences
                ?.Select(f => new FeeHomeAssistance(f.Price, f.Radius));

            var files = request.Images
                ?.OrderBy(i => i.Order)
                ?.Where(i => i.Image is not null)
                ?.Select(i => (i.Image!.OpenReadStream(), i.Image.FileName))
                ?.ToList();

            var images = files is not null
                ? await _storageService.AddAsync(files, cancellationToken)
                : null;

            var tags = request.Tags is not null
                ? await _unitOfWork.TagRepository.GetByIdsAsync(request.Tags)
                : null;

            var service = new Service(request.Name, request.Description, request.Price, 
                request.SupplierId, request.SubCategoryId, request.HasHomeAssistance,
                images?.ToList(), feeHomeAssistences?.ToList(), tags?.ToList());

            await _unitOfWork.ServiceRepository.AddAsync(service, cancellationToken);
            await _unitOfWork.CommitAsync();

            return ValidationResult;
        }
    }
}
