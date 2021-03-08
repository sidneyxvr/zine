using Argon.Core.Internationalization;
using FluentValidation;

namespace Argon.Core.Messages.IntegrationCommands.Validators
{
    public abstract class BaseValidator<T> : AbstractValidator<T>
    {
        protected Localizer Localizer;

        public BaseValidator()
        {
            Localizer = Localizer.GetLocalizer();
        }
    }
}
