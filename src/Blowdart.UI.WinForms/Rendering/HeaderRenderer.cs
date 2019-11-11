// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Drawing;
using System.Windows.Forms;
using Blowdart.UI.Instructions;

namespace Blowdart.UI.WinForms.Rendering
{
	internal sealed class HeaderRenderer : IRenderer<HeaderInstruction, Panel>
	{
		public void Render(Panel renderer, HeaderInstruction instruction)
		{
			var header = (HeaderInstruction) instruction;

			var label = new Label
			{
				Text = header.Text,
				AutoSize = true
			};

			var emSize = header.Level switch
			{
				1 => 32f,		// 2em,
				2 => 24f,		// 1.5em
				3 => 18.72f,	// 1.17em
				4 => 16,		// 1em
				5 => 13.28f,    // 0.83em
				6 => 10.72f,    // 0.67em
				_ => throw new BlowdartException("Headers can only have six levels of rank")
			};

			label.Font = new Font(label.Font.Name, emSize);
			renderer.Controls.Add(label);
		}
	}
}