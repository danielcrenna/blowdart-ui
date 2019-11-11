// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI;
using Blowdart.UI.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Examples.Pages
{
	public class LocalizationPage
	{
		public static void Index(Ui ui)
		{
			var store = ui.GetRequiredService<ILocalizationStore>();

			ui.Header(1, "Localization");

			var all = store.GetAll();

			ui.ListTable(all, (i, entry) =>
			{
				ui.Text(entry.Tag);
				ui.NextColumn();
				ui.Text(entry.Value);
				ui.NextColumn();
				ui.CheckBox(entry.IsMissing);
			});
		}
	}
}