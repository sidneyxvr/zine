using Argon.Catalog.Application.Commands;
using Argon.Catalog.Domain;
using Argon.Core.Messages;
using FluentValidation.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Application.CommandHandlers
{
    public class CreateServiceHandler : RequestHandler<CreateServiceCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateServiceHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override Task<ValidationResult> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
