// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Blowdart.UI.Instructions;
using ImGuiNET;

namespace Blowdart.UI.Game
{
	internal sealed class ImGuiRenderer
	{
		public static void RenderUi(Ui ui, Action<Ui> handler)
		{
			ui.Begin();
			handler(ui);
			foreach (var instruction in ui.Instructions)
			{
				switch (instruction)
				{
					case TextInstruction text:
						ImGui.TextUnformatted(text.Text);
						break;
					case ButtonInstruction button:
						ImGui.PushID(button.Id.ToString());
						ImGui.Button(button.Text);
						ImGui.PopID();
						break;
				}
			}
		}
	}
}