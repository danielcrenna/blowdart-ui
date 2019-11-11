// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Windows.Forms;
using Blowdart.UI.Instructions;

namespace Blowdart.UI.WinForms.Rendering
{
	internal sealed class MenuItemRenderer : IRenderer<MenuItemInstruction, Panel>
	{
		public void Render(Panel renderer, MenuItemInstruction instruction)
		{
			var item = (MenuItemInstruction) instruction;
			// var form = (Form) panel.Parent;

			var menuItem = new ToolStripMenuItem(item.Title);
			foreach (var control in renderer.Controls)
			{
				if (control is ToolStrip strip)
				{
					strip.Items.Add(menuItem);
				}
			}

			//form.Menu.MenuItems.Add(menuItem);
		}
	}
}