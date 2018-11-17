using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Localizer
{
    public class LocalizerConfig
    {
        public LocalizerConfig(bool overrideKeyValues = false)
        {
            OverrideKeyValues = overrideKeyValues;
            Translations = new Dictionary<string, Dictionary<string, string>>();
        }

        public bool OverrideKeyValues { get; set; }

        internal Dictionary<string, Dictionary<string, string>> Translations { get; }

        public void AddFile(string path)
        {
            AddFile(path, CultureInfo.CurrentCulture.Name);
        }

        public void AddFile(string path, string localeCode)
        {
            string fileContent = File.ReadAllText(path, Encoding.UTF8);
            Dictionary<string, string> parsedDictionary =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(fileContent);
            AddTranslationsToDictionary(localeCode, parsedDictionary);
        }

        private void AddTranslationsToDictionary(string localeCode, Dictionary<string, string> parsedDictionary)
        {
            foreach (string key in parsedDictionary.Keys)
            {
                if (!Translations.ContainsKey(key))
                    Translations.Add(key, new Dictionary<string, string>());

                if (Translations[key].ContainsKey(localeCode) == false)
                    Translations[key].Add(localeCode, parsedDictionary[key]);

                if (!OverrideKeyValues)
                    continue;

                Translations[key][localeCode] = parsedDictionary[key];
            }
        }
    }
}