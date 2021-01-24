using Argon.Core.Internationalization;
using FluentValidation;

namespace Argon.Customers.Application.Commands.Validations
{
    public abstract class BaseValidation<T> : AbstractValidator<T>
    {
        protected Localizer Localizer;

        public BaseValidation()
        {
            Localizer = Localizer.GetLocalizer();
        }
    }
}
