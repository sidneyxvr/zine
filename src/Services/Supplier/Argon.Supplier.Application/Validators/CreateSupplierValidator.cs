using Argon.Core.Messages.IntegrationCommands;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Argon.Suppliers.Application.Validators
{
    public class CreateSupplierValidator : AbstractValidator<CreateSupplierCommand>
    {
        public CreateSupplierValidator(IStringLocalizer<CreateSupplierValidator> localizer)
        {

        }
    }
}
