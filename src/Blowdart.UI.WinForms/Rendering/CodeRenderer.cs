// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Drawing;
using System.Windows.Forms;
using Blowdart.UI.Instructions;
using Blowdart.UI.Internal;

namespace Blowdart.UI.WinForms.Rendering
{
	internal sealed class CodeRenderer : IRenderer<CodeInstruction, Panel>
	{
		public void Render(Panel p, CodeInstruction instruction)
		{
			var code = instruction;

			var text = code.Value.FormatCode();

			var textBox = new TextBox
			{
				Text = text,
				AutoSize = false,
				Multiline = code.Block,
				ReadOnly = true,
				Enabled = false
			};

			var size = System.Windows.Forms.TextRenderer.MeasureText(text, textBox.Font, Size.Empty,
				TextFormatFlags.Default);

			textBox.Width = size.Width + 20;
			textBox.Height = size.Height + 10;

			p.Controls.Add(textBox);
		}
	}
}