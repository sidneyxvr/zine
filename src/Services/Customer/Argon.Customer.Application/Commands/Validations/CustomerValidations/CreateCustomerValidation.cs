using Argon.Customers.Application.Commands.CustomerCommands;
using FluentValidation;

namespace Argon.Customers.Application.Commands.Validations.CustomerValidations
{
    public class CreateCustomerValidation : BaseValidation<CreateCustomerCommand>
    {
        public CreateCustomerValidation()
        {
            RuleFor(c => c.FullName)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyFullName"));
        }
    }
}
