using Argon.Catalog.Application.Commands;
using Argon.Catalog.Domain;
using Argon.Core.Messages;
using FluentValidation.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Application.CommandHandlers
{
    public class CreateDepartmentHandler : RequestHandler<CreateDepartmentCommand>
    {
        public override Task<ValidationResult> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
