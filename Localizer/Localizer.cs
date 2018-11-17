using System.Collections.Generic;
using System.Threading;

namespace Localizer
{
    public class Localizer
    {
        private Dictionary<string, Dictionary<string, string>> _translations;

        private Localizer()
        {
            _translations = new Dictionary<string, Dictionary<string, string>>();
        }

        public static Localizer Setup(LocalizerConfig localizerConfig)
        {
            Localizer instance = new Localizer
            {
                _translations = localizerConfig.Translations
            };
            return instance;
        }

        public string this[string key]
        {
            get
            {
                try
                {
                    return _translations[key][Thread.CurrentThread.CurrentCulture.Name];
                }
                catch (KeyNotFoundException)
                {
                    return key;
                }
            }
        }

        public string this[string key, string langCode]
        {
            get
            {
                try
                {
                    return _translations[key][langCode];
                }
                catch (KeyNotFoundException)
                {
                    return key;
                }
            }
        }
    }
}