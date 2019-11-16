// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Drawing;
using System.Windows.Forms;
using Blowdart.UI.Instructions;

namespace Blowdart.UI.WinForms.Rendering
{
	internal sealed class CheckBoxRenderer : IRenderer<CheckBoxInstruction, Panel>
	{
		private readonly ImGui _imGui;

		public CheckBoxRenderer(ImGui imGui)
		{
			_imGui = imGui;
		}

		public void Render(Panel p, CheckBoxInstruction instruction)
		{
			var checkBox = new CheckBox
			{
				CheckAlign = instruction.Alignment switch
				{
					ElementAlignment.Left => ContentAlignment.MiddleLeft,
					ElementAlignment.Right => ContentAlignment.MiddleRight,
					_ => throw new ArgumentOutOfRangeException()
				},
				Text = instruction.Text
			};
			checkBox.Click += _imGui.OnClickCallback(instruction.Id);
			p.Controls.Add(checkBox);
		}
	}
}