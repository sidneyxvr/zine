using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Argon.Core.Internationalization
{
    public sealed class Localizer 
    {
        private static readonly List<JsonLocalization> _languages = new List<JsonLocalization>();

        private Localizer()
        {
            _languages.Add(new JsonLocalization
            {
                Name = "en-US",
                Resources =
                    JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(@"i18n.en-US.json", Encoding.UTF8))
            });

            _languages.Add(new JsonLocalization
            {
                Name = "pt-BR",
                Resources =
                    JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText($"i18n.pt-BR.json", Encoding.GetEncoding("iso-8859-1")))
            });
        }

        public static string GetValue(string name)
        {
            return _languages
                .FirstOrDefault(l => l.Name == CultureInfo.CurrentCulture.Name)?
                .Resources[name]!;
        }
    }

    public class JsonLocalization
    {
        public string Name { get; set; }
        public Dictionary<string, string> Resources = new Dictionary<string, string>();
    }
}
