using Argon.Catalog.Application.Commands;
using Argon.Catalog.Domain;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Argon.Catalog.Application.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator(IStringLocalizer<CreateProductValidator> localizer)
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(localizer["Required Name"])
                .MaximumLength(Product.NameMaxLength).WithMessage(localizer["Name's Max Length"]);

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(localizer["Required Name"])
                .MaximumLength(Product.NameMaxLength).WithMessage(localizer["Name's Max Length"]);

            //RuleFor(p => p.Image)
            //    .NotEmpty().WithMessage(localizer["Required Image"]);
        }
    }
}
