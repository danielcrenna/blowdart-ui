// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Windows.Forms;
using Blowdart.UI.Instructions;

namespace Blowdart.UI.WinForms.Rendering
{
	internal sealed class SliderRenderer : IRenderer<SliderInstruction, Panel>
	{
		public void Render(Panel p, SliderInstruction instruction)
		{
			var slider = new TrackBar();
			slider.Text = instruction.Text;
			p.Controls.Add(slider);
		}
	}
}