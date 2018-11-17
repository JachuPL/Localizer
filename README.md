# Localizer
[![NuGet version (Localizer)](https://img.shields.io/nuget/v/Localizer.svg?style=flat-square)](https://www.nuget.org/packages/Localizer/)
Simple cross-platform localization utility for your .NET apps, compatible with .NET Standard 2.0.

## Installation
You can use Localizer as a dependency in your application by referencing it as a NuGet package.

## Usage
### Configuration
#### Creating configuration object
First of all, you have to configure your localization settings. For that, you have to provide localization files in the following format
```
{
	"Default string": "Translation of default string in configured language",
    ...
	"Another default string": "Translation of another default string"
}
```

Then, you have to create `LocalizerConfig` instance and add your file. The following configuration will configure the file to be used as current culture (`Thread.CurrentThread.CurrentCulture.Name`) translation:
```
LocalizerConfig configuration = new LocalizerConfig();
configuration.AddFile("Translations/locale.en-us.json");
```

If you want to configure translation file to be used for specific culture, you can do it by passing additional string parameter containing IETF Language Tag (e.g. en-US, de-DE, pl-PL etc.) like below:
```
configuration.AddFile("Translations/locale.pl-pl.json", "pl-PL");
```

#### Overriding imported strings
Since you are able to import multiple files with translations for single language, there's an option on how to deal with conflicts (occuring when you have two different definitions of one key). By default, Localizer does not allow definitions to be overriden, so the first found definition is used. You can change it to last definition being used by passing additional `bool` parameter to `LocalizerConfig`'s constructor like so:
```
LocalizerConfig configuration = new LocalizerConfig(true);
```
This is also possible by using a property, but **it has no effect if json files were already loaded!**.

#### Creating Localizer instance
Now, when your configuration object is ready, you can create your Localizer:
```
Localizer localizer = Localizer.Setup(configuration);
```
#### Using Localizer
Using Localizer is really simple. When you need to translate something to current culture, you do the following:
```
Console.WriteLine(localizer["This is just a test"]);
```

If you need to translate something using different culture, you have to pass IETF Language Tag as a second indexer parameter:
```
Console.WriteLine(localizer["This is just a test", "pl-PL"]);
```
## Third Party software used by Localizer
Localizer relies on the following software:
* [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) is used for parsing json files. **This might change in the future versions**.
* [Nunit](https://github.com/nunit/nunit) is used to run unit tests.

For this software, author of Localizer gives absolutely no warranty and is not responsible of their behaviour.