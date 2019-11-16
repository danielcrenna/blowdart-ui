// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Windows.Forms;
using Blowdart.UI.Instructions;

namespace Blowdart.UI.WinForms.Rendering
{
	internal sealed class SeparatorRenderer : IRenderer<SeparatorInstruction, Panel>
	{
		public void Render(Panel p, SeparatorInstruction instruction)
		{
			p.Controls.Add(new Label
			{
				Text = "",
				BorderStyle = BorderStyle.Fixed3D,
				AutoSize = false,
				Width = p.Width,
				Height = 2
			});
		}
	}
}