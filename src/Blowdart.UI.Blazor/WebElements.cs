// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace

using System;
using Blowdart.UI;

// FIXME: use source generators or something similar

public static class WebElements
{
	#region Inline Text Directives

	public static void h1(this Ui ui, string text) => InlineTextDirective(ui, "h1", text);
	public static void p(this Ui ui, string text) => InlineTextDirective(ui, "p", text);
		
	private static void InlineTextDirective(Ui ui, string element, string text)
	{
		ui.BeginElement(element);
		ui.Text(text);
		ui.EndElement(element);
	}

	#endregion

	#region Inline Wrapped Element Closures

	public static void Div(this Ui ui, Action<Ui> closure)
	{
		ui.BeginElement("div");
		closure?.Invoke(ui);
		ui.EndElement("div");
	}

	#endregion
}