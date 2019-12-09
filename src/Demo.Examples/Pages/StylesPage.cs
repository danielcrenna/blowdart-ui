// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Blowdart.UI;

namespace Demo.Examples.Pages
{
	public class StylesPage
	{
		private static readonly OpenIconicIcons[] Icons = (OpenIconicIcons[]) Enum.GetValues(typeof(OpenIconicIcons));

		public static void Index(Ui ui)
		{
			ui.Header(1, "Icons");

			ui.ListTable(Icons.InGroupsOf(5), (i, icons) =>
			{
				foreach (var icon in icons)
				{
					ui.InlineIcon(icon);
					ui.NextColumn(ref i);
					ui.Text(icon.ToIconCase());
					ui.NextColumn(ref i);
				}
			});

			#region Code

			ui.SampleCode(@"
ui.Header(1, ""Icons"");

ui.ListTable(Icons.InGroupsOf(5), icons =>
{
	foreach (var icon in icons)
	{
		ui.InlineIcon(icon);
		ui.NextColumn();
		ui.Text(icon.ToIconCase());
		ui.NextColumn();
	}
});");
			#endregion
		}
	}
}