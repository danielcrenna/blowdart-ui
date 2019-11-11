// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Globalization;

namespace Blowdart.UI.Localization
{
	public class LocalizationProvider : ILocalizationProvider
	{
		public static string DefaultTwoLetterIsoLanguageName = "en";

		private readonly ILocalizationStore _store;
		private readonly ILocaleResolver _localeResolver;

		public LocalizationProvider(ILocalizationStore store, ILocaleResolver localeResolver)
		{
			_store = store;
			_localeResolver = localeResolver;
		}

		public string GetText(string key)
		{
			var languages = _localeResolver.GetLanguagePreferences();
			var language = GetBestEffortLanguage(languages);
			var value = _store.GetValue(key, language);
			return value.Value;
		}

		private string GetBestEffortLanguage(IEnumerable<string> languages)
		{
			foreach (var language in languages)
			{
				var culture = GetCultureInfoFromLanguage(language);
				if (_store.IsLanguageAvailable(culture))
					return culture.IetfLanguageTag;
			}
			return DefaultTwoLetterIsoLanguageName;
		}

		private static CultureInfo GetCultureInfoFromLanguage(string language)
		{
			return new CultureInfo(language, true);
		}
	}
}