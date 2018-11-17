using FluentAssertions;
using NUnit.Framework;

namespace Localizer.UnitTests
{
    [TestFixture]
    public class LocalizerConfigTests
    {
        private const string translationKey = "Dane testowe";

        [Test]
        public void WhenOverridingTranslationsLastTranslationShouldBePicked()
        {
            // given
            LocalizerConfig config = new LocalizerConfig(true);
            config.AddFile(@"Translations/locale.en-us.test.json", "en-US");
            config.AddFile(@"Translations/locale.en-us.test.copy.json", "en-US");
            Localizer localizer = Localizer.Setup(config);

            // when
            string latestTranslation = localizer[translationKey, "en-US"];

            // then
            latestTranslation.Should().Be("Test-related data");
        }

        [Test]
        public void WhenOverridingIsForbiddenFirstTranslationShouldBePicked()
        {
            // given
            LocalizerConfig config = new LocalizerConfig();
            config.AddFile(@"Translations/locale.en-us.test.json", "en-US");
            config.AddFile(@"Translations/locale.en-us.test.copy.json", "en-US");
            Localizer localizer = Localizer.Setup(config);

            // when
            string latestTranslation = localizer[translationKey, "en-US"];

            // then
            latestTranslation.Should().Be("Test data");
        }
    }
}