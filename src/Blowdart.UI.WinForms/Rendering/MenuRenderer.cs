// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Windows.Forms;
using Blowdart.UI.Instructions;

namespace Blowdart.UI.WinForms.Rendering
{
	internal sealed class MenuRenderer : 
		IRenderer<BeginMenuInstruction, Panel>
	{
		public void Render(Panel p, BeginMenuInstruction instruction)
		{
			var beginMenu = (BeginMenuInstruction) instruction;

			var menu = new ToolStrip();
			menu.Dock = DockStyle.Top;

			p.Controls.Add(menu);
		}
	}
}