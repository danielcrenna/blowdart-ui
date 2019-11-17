// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Android.Views;
using Android.Widget;
using Blowdart.UI;
using Blowdart.UI.Instructions;

namespace Blowdart.Ui.Rendering
{
	internal sealed class TextBlockRenderer : IRenderer<TextBlockInstruction, ViewGroup>
	{
		public void Render(ViewGroup v, TextBlockInstruction instruction)
		{
			var text = new TextView(v.Context);
			text.Text = instruction.Text;

			v.AddView(text);
		}
	}
}