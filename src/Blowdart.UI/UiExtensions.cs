// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Blowdart.UI.Instructions;

namespace Blowdart.UI;

public static class UiExtensions
{
	public static void BeginElement(this Ui ui, string name, UInt128? id = default)
	{
		ui.Add(new BeginElementInstruction(name, id));

		while (ui.TryPopAttribute(out var attribute))
			ui.Attribute(attribute.name, attribute.value);

		if (!ui.TryPopStyle(out var style))
			return;

		var context = new StyleContext();
		style?.Invoke(context);
		ui.Attribute(HtmlAttributes.Class, context);
	}

	public static void EndElement(this Ui ui, string name) => ui.Add(new EndElementInstruction(name));

	public static void Attribute(this Ui ui, object key, object value) => ui.Add(new AttributeInstruction(key, value));

	public static void _(this Ui ui, object text) => ui.Add(new TextInstruction(text));

	public static void Log(this Ui ui, string message) => ui.Add(new LogInstruction(message));
}