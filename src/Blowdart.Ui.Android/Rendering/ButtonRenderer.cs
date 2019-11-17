// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Android.App;
using Android.Views;
using Android.Widget;
using Blowdart.UI;
using Blowdart.UI.Instructions;

namespace Blowdart.Ui.Rendering
{
	internal sealed class ButtonRenderer : IRenderer<ButtonInstruction, ViewGroup>
	{
		private readonly ImGui _imGui;

		public ButtonRenderer(ImGui imGui)
		{
			_imGui = imGui;
		}

		public void Render(ViewGroup v, ButtonInstruction instruction)
		{
			var button = new Button(v.Context);
			button.Click += _imGui.OnClickCallback(instruction.Id);
			button.Text = instruction.Text;
			v.AddView(button);
		}
	}
}