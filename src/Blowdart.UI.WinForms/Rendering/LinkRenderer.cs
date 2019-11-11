// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Windows.Forms;
using Blowdart.UI.Instructions;

namespace Blowdart.UI.WinForms.Rendering
{
	internal sealed class LinkRenderer : IRenderer<LinkInstruction, Panel>
	{
		public void Render(Panel renderer, LinkInstruction instruction)
		{
			var link = (LinkInstruction) instruction;
			var label = new LinkLabel { Text = link.Title, AutoSize = true };
			label.LinkArea = new LinkArea(0, link.Title.Length);
			label.LinkBehavior = LinkBehavior.AlwaysUnderline;
			label.Links.Add(new LinkLabel.Link());
			renderer.Controls.Add(label);
		}
	}
}