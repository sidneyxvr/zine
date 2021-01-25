using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Argon.Core.Internationalization
{
    public sealed class Localizer
    {
        private static readonly Localizer Instance = new();

        private readonly List<JsonLocalization> _languages;

        private Localizer()
        {
            var pathBase = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            const string folder = "Internationalization";

            _languages = new List<JsonLocalization>
            {
                new JsonLocalization
                {
                    Name = "en-US",
                    Resources =
                    JsonSerializer.Deserialize<Dictionary<string, string>>(
                        File.ReadAllText(
                            Path.Combine(pathBase, folder, "i18n.en-US.json"), 
                            Encoding.UTF8))
                },

                new JsonLocalization
                {
                    Name = "pt-BR",
                    Resources =
                    JsonSerializer.Deserialize<Dictionary<string, string>>(
                        File.ReadAllText(
                            Path.Combine(pathBase, folder, "i18n.pt-BR.json"), 
                            Encoding.GetEncoding("iso-8859-1")))
                }
            };
        }

        public static Localizer GetLocalizer()
        {
            return Instance;
        }

        public string GetTranslation(string name)
        {
            return _languages
                .FirstOrDefault(l => l.Name.Equals(CultureInfo.CurrentCulture.Name, StringComparison.OrdinalIgnoreCase))?
                .Resources[name]!;
        }
    }

    public class JsonLocalization
    {
        public string Name { get; set; }
        public Dictionary<string, string> Resources = new();
    }
}
