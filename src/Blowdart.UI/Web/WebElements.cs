// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace

using System;
using System.Diagnostics.CodeAnalysis;
using Blowdart.UI;

[ExcludeFromCodeCoverage]
public static partial class WebElements
{
	private static ElementRef InlineDirective(Ui ui, string element, object inner, Value128? id = default)
	{
		ui.BeginElement(element, id);
		ui._(inner);
		ui.EndElement(element);
		return new ElementRef(ui, id);
	}

	public static ElementRef NestedDirective(Ui ui, string element, Action<Ui> closure, Value128? id = default)
	{
		ui.BeginElement(element, id);
		closure?.Invoke(ui);
		ui.EndElement(element);
		return new ElementRef(ui, id);
	}
}