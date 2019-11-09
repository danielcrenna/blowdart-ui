// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Windows.Forms;

namespace Blowdart.UI.WinForms.Rendering
{
	internal sealed class SeparatorRenderer : IFormRenderer
	{
		public void Render(RenderInstruction instruction, Panel panel)
		{
			panel.Controls.Add(new Label
			{
				Text = "", 
				BorderStyle = BorderStyle.Fixed3D, 
				AutoSize = false, 
				Width = panel.Width,
				Height = 2
			});
		}
	}
}