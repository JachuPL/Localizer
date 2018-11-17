using FluentAssertions;
using NUnit.Framework;
using System.Globalization;
using System.Threading;

namespace Localizer.UnitTests
{
    [TestFixture]
    public class LocalizerTests
    {
        private const string translationKey = "Dane testowe";

        [Test]
        public void ReturnsKeyStringWhenLanguageIsNotConfigured()
        {
            // given
            LocalizerConfig config = new LocalizerConfig(true);
            config.AddFile(@"Translations\locale.en-us.test.json", "en-US");
            config.AddFile(@"Translations\locale.de-de.test.json", "de-DE");
            Localizer localizer = Localizer.Setup(config);

            // when
            string frenchTranslation = localizer[translationKey, "fr-FR"];

            // then
            frenchTranslation.Should().Be(translationKey);
        }

        [Test]
        public void ReturnsKeyStringWhenTranslationToSpecifiedLanguageIsNotFound()
        {
            // given
            const string key = "Nie ma tego z³ego, co by nie wysz³o na dobre";
            LocalizerConfig config = new LocalizerConfig(true);
            config.AddFile(@"Translations\locale.en-us.test.json", "en-US");
            config.AddFile(@"Translations\locale.de-de.test.json", "de-DE");
            Localizer localizer = Localizer.Setup(config);

            // when
            string englishTranslation = localizer[key, "en-US"];

            // then
            englishTranslation.Should().Be(key);
        }

        [Test]
        public void ReturnsKeyStringWhenTranslationToCurrentCultureLanguageIsNotFound()
        {
            // given
            const string key = "Nie ma tego z³ego, co by nie wysz³o na dobre";
            LocalizerConfig config = new LocalizerConfig(true);
            config.AddFile(@"Translations\locale.en-us.test.json", "en-US");
            config.AddFile(@"Translations\locale.de-de.test.json", "de-DE");
            Localizer localizer = Localizer.Setup(config);

            // when
            string currentCultureTranslation = localizer[key];

            // then
            currentCultureTranslation.Should().Be(key);
        }

        [Test]
        public void ReturnsTranslationWhenTranslationToSpecificLanguageIsFound()
        {
            // given
            LocalizerConfig config = new LocalizerConfig(true);
            config.AddFile(@"Translations\locale.en-us.test.json", "en-US");
            config.AddFile(@"Translations\locale.de-de.test.json", "de-DE");
            Localizer localizer = Localizer.Setup(config);

            // when
            string englishTranslation = localizer[translationKey, "en-US"];
            string germanTranslation = localizer[translationKey, "de-DE"];

            // then
            englishTranslation.Should().Be("Test data");
            germanTranslation.Should().Be("Testdatei");
        }

        [Test]
        public void ReturnsTranslationWhenTranslationToCurrentCultureLanguageIsFound()
        {
            // given
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            LocalizerConfig config = new LocalizerConfig(true);
            config.AddFile(@"Translations\locale.en-us.test.json");
            Localizer localizer = Localizer.Setup(config);

            // when
            string currentCultureTranslation = localizer[translationKey];

            // tthen
            currentCultureTranslation.Should().Be("Test data");
        }
    }
}