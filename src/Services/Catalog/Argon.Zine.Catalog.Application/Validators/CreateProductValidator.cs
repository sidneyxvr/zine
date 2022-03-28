using Argon.Zine.Catalog.Application.Commands;
using Argon.Zine.Catalog.Domain;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Argon.Zine.Catalog.Application.Validators;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator(IStringLocalizer<CreateProductValidator> localizer)
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage(localizer["Required Name"])
            .MaximumLength(Product.NameMaxLength).WithMessage(localizer["Name's Max Length"]);

        RuleFor(p => p.Description)
            .NotEmpty().WithMessage(localizer["Required Name"])
            .MaximumLength(Product.DescriptionMaxLength).WithMessage(localizer["Name's Max Length"]);
    }
}