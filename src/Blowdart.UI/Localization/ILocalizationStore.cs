// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Globalization;

namespace Blowdart.UI.Localization
{
	public interface ILocalizationStore
	{
		bool IsLanguageAvailable(CultureInfo culture);
		Localization GetValue(string key, string language);
		IEnumerable<Localization> GetAll();
	}
}