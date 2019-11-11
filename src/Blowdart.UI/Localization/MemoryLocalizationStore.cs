// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Globalization;

namespace Blowdart.UI.Localization
{
	public class MemoryLocalizationStore : ILocalizationStore
	{
		private readonly Dictionary<string, List<Localization>> _store;

		public MemoryLocalizationStore()
		{
			_store = new Dictionary<string, List<Localization>>();
		}

		public bool IsLanguageAvailable(CultureInfo culture)
		{
			return false;
		}

		public Localization GetValue(string key, string language)
		{
			if (!_store.TryGetValue(key, out var list))
				_store.Add(key, list = new List<Localization>());

			foreach (var entry in list)
			{
				if (entry.Tag == language)
					return entry;
			}

			var missing = new Localization
			{
				Value = key,
				Tag = language,
				IsMissing = true
			};

			list.Add(missing);
			return missing;
		}

		public IEnumerable<Localization> GetAll()
		{
			var keys = new List<string>(_store.Keys);

			foreach (var key in keys)
			{
				foreach (var entry in _store[key])
				{
					yield return entry;
				}
			}
		}
	}
}