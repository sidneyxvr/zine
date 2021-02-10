using Argon.Core.Internationalization;

namespace Argon.Core.Messages
{
    public class BaseHandler
    {
        protected Localizer Localizer;

        public BaseHandler()
        {
            Localizer = Localizer.GetLocalizer();
        }
    }
}
