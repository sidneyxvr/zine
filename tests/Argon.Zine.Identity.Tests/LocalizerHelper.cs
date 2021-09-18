using Argon.Zine.Identity.Services;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;

namespace Argon.Zine.Identity.Tests
{
    public static class LocalizerHelper
    {
        public static IStringLocalizer CreateInstanceStringLocalizer(Type resourceSource)
        {
            var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
            return new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance).Create(resourceSource);
        }

        public static StringLocalizer<T> CreateInstanceStringLocalizer<T>()
            where T : BaseService
        {
            var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            return new StringLocalizer<T>(factory);
        }
    }
}
