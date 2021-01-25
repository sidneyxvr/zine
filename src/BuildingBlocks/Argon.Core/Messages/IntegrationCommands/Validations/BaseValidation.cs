using Argon.Core.Internationalization;
using FluentValidation;

namespace Argon.Core.Messages.IntegrationCommands.Validations
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
