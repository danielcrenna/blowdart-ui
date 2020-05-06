// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI;
using Blowdart.UI.Instructions;

// ReSharper disable CheckNamespace

public static class WebUiExtensions
{
	public static bool Button(this Ui ui, string text)
	{
		var id = ui.NextId();
		ui.Add(new ButtonInstruction(id, text));
		return ui.OnEvent("onclick", id, out _);
	}
}