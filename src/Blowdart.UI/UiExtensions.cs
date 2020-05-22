// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;

namespace Blowdart.UI
{
	public static class UiExtensions
	{
		public static void BeginElement(this Ui ui, string name)
		{
			ui.Add(new BeginElementInstruction(name));
		}

		public static void EndElement(this Ui ui, string name)
		{
			ui.Add(new EndElementInstruction(name));
		}

		public static void _(this Ui ui, object text)
		{
			ui.Add(new TextInstruction(text));
		}
		
		public static void Log(this Ui ui, string message)
		{
			ui.Add(new LogInstruction(message));
		}

		#region Button

		public static bool Button(this Ui ui, string text)
		{
			var id = ui.NextId();
			ui.Add(new ButtonInstruction(id, text));
			return ui.OnEvent("onclick", id, out var _);
		}

		#endregion
	}
}