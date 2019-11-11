// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Globalization;

namespace Blowdart.UI.Localization
{
	public class MemoryLocalizationStore : ILocalizationStore
	{
		private readonly Dictionary<string, List<string>> _store;

		public MemoryLocalizationStore()
		{
			_store = new Dictionary<string, List<string>>();
		}

		public bool IsLanguageAvailable(CultureInfo culture)
		{
			return false;
		}

		public Localization GetValue(string key, string language)
		{
			return new Localization {Value = key, Tag = language};
		}
	}
}