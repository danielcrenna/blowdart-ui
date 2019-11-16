// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Windows.Forms;
using Blowdart.UI.Instructions;

namespace Blowdart.UI.WinForms.Rendering
{
	internal sealed class ButtonRenderer : IRenderer<ButtonInstruction, Panel>
	{
		private readonly ImGui _imGui;

		public ButtonRenderer(ImGui imGui)
		{
			_imGui = imGui;
		}

		public void Render(Panel p, ButtonInstruction instruction)
		{
			var button = new Button {Text = instruction.Text};
			button.Click += _imGui.OnClickCallback(instruction.Id);
			p.Controls.Add(button);
		}
	}
}