// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// ReSharper disable InconsistentNaming

namespace Blowdart.UI.Blazor
{
	public static class WebElements
	{
		public static void h1(this Ui ui, string text) => InlineDirective(ui, "h1", text);
		public static void p(this Ui ui, string text) => InlineDirective(ui, "p", text);

		private static void InlineDirective(Ui ui, string element, string text)
		{
			ui.BeginElement(element);
			ui.Text(text);
			ui.EndElement(element);
		}
	}
}