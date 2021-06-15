using Argon.Catalog.Domain;
using Argon.Core.Messages.IntegrationCommands.Validators;
using FluentValidation;

namespace Argon.Catalog.Application.Commands.Validators
{
    public class CreateServiceValidator : BaseValidator<CreateServiceCommand>
    {
        public CreateServiceValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyName"))
                .MaximumLength(Service.NameMaxLength).WithMessage(Localizer.GetTranslation("NameMaxLength"));

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyName"))
                .MaximumLength(Service.NameMaxLength).WithMessage(Localizer.GetTranslation("NameMaxLength"));

            RuleFor(p => p.Images)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyImage"));

            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyCategory"));
        }
    }
}
