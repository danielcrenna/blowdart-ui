// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Windows.Forms;
using Blowdart.UI.Instructions;

namespace Blowdart.UI.WinForms.Rendering
{
	internal sealed class MenuItemRenderer : IRenderer<MenuItemInstruction, Panel>
	{
		private readonly ImGui _imGui;

		public MenuItemRenderer(ImGui imGui)
		{
			_imGui = imGui;
		}

		public void Render(Panel p, MenuItemInstruction instruction)
		{
			var item = instruction;
			
			var menuItem = new ToolStripMenuItem(item.Title);
			menuItem.Click += (sender, args) => { _imGui.ChangePage(instruction.Template); };
			foreach (var control in p.Controls)
			{
				if (control is ToolStrip strip)
				{
					strip.Items.Add(menuItem);
				}
			}
		}
	}
}