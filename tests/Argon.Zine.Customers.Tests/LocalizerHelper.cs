using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace Argon.Zine.Customers.Tests;

public static class LocalizerHelper
{
    public static IStringLocalizer<IValidator> CreateInstanceStringLocalizer<IValidator>()
    {
        var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
        var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
        return new StringLocalizer<IValidator>(factory);
    }
}