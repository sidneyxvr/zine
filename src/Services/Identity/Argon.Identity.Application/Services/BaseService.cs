using Argon.Core.Internationalization;

namespace Argon.Identity.Application.Services
{
    public class BaseService
    {
        protected Localizer Localizer;

        public BaseService()
        {
            Localizer = Localizer.GetLocalizer();
        }
    }
}
